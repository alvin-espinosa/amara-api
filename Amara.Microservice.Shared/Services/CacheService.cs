using Amara.Microservice.Shared.DTOs;
using Amara.Microservice.Shared.Interface;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using StackExchange.Redis;
using System.Net.Sockets;

namespace Amara.Microservice.Shared.Services
{
    public class CacheService : ICacheService
    {
        private IConnectionMultiplexer connectionMultiplexer;
        private readonly ILogger<CacheService> logger;
        private IDatabase database;
        private CacheConfig cacheConfig;

        private long lastReconnectTicks = DateTimeOffset.MinValue.UtcTicks;
        private SemaphoreSlim reconnectSemaphore = new SemaphoreSlim(initialCount: 1, maxCount: 1);
        private DateTimeOffset firstErrorTime = DateTimeOffset.MinValue;
        private DateTimeOffset previousErrorTime = DateTimeOffset.MinValue;


        public CacheService(
            IConnectionMultiplexer connectionMultiplexer,
            IOptions<CacheConfig> options,
            ILogger<CacheService> logger)
        {
            if (options is null)
            {
                throw new ArgumentNullException(nameof(options));
            }
            else
            {
                this.cacheConfig = options.Value;
            }
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.connectionMultiplexer = connectionMultiplexer ?? throw new ArgumentNullException(nameof(connectionMultiplexer));
            this.database = connectionMultiplexer.GetDatabase();
        }

        public async Task<T> GetFromCache<T>(string key)
        {
            int reconnectRetry = 0;

            while (true)
            {
                try
                {
                    var redisValue = await database.StringGetAsync(key);

                    return string.IsNullOrEmpty(redisValue) ? default : JsonConvert.DeserializeObject<T>(redisValue);
                }
                catch (Exception ex) when (ex is RedisConnectionException || ex is SocketException || ex is ObjectDisposedException)
                {
                    reconnectRetry++;

                    if (reconnectRetry > cacheConfig.RetryMaxAttempts)
                    {
                        throw;
                    }

                    try
                    {
                        await ForceReconnectAsync();
                    }
                    catch
                    {
                        logger.LogError($"Unable to get object from Redis with Key [{key}]", ex);
                    }
                }
            }

        }

        public async Task SetToCache<T>(
            string key,
            T value,
            int? customExpirationInHours = null,
            JsonSerializerSettings? serializerSettings = null)
        {
            int reconnectRetry = 0;

            while (true)
            {
                try
                {
                    // expires at the custom specified one, or default set to config
                    TimeSpan actualExpiration = DateTimeOffset.Now
                        .AddHours(customExpirationInHours ?? cacheConfig.DefaultExpirationHours) - DateTimeOffset.Now;

                    string cacheValue = serializerSettings == null ? 
                        JsonConvert.SerializeObject(value) : 
                        JsonConvert.SerializeObject(value, serializerSettings);

                    var success = await database.StringSetAsync(key, cacheValue, actualExpiration);

                    if (success)
                    {
                        logger.LogError($"Set object to Redis with Key [{key}]");                        
                    }
                    else
                    {
                        logger.LogError($"Unable to set object to Redis with Key [{key}]");
                    }

                    return;
                }
                catch (Exception ex) when (ex is RedisConnectionException || ex is SocketException || ex is ObjectDisposedException)
                {
                    reconnectRetry++;
                    if (reconnectRetry > cacheConfig.RetryMaxAttempts)
                    {
                        throw;
                    }

                    try
                    {
                        await ForceReconnectAsync();
                    }
                    catch (ObjectDisposedException)
                    {
                        logger.LogError($"Unable to set object to Redis with Key [{key}]", ex);
                    }
                }
            }
        }

        private async Task ForceReconnectAsync(bool initializing = false)
        {
            long previousTicks = Interlocked.Read(ref lastReconnectTicks);
            var previousReconnectTime = new DateTimeOffset(previousTicks, TimeSpan.Zero);
            TimeSpan elapsedSinceLastReconnect = DateTimeOffset.UtcNow - previousReconnectTime;

            // We want to limit how often we perform this top-level reconnect, so we check how long it's been since our last attempt.
            if (elapsedSinceLastReconnect < TimeSpan.FromSeconds(cacheConfig.ReconnectMinInterval))
            {
                return;
            }

            bool lockTaken = await reconnectSemaphore.WaitAsync(TimeSpan.FromSeconds(cacheConfig.RestartConnectionTimeout));
            if (!lockTaken)
            {
                // If we fail to enter the semaphore, then it is possible that another thread has already done so.
                // ForceReconnectAsync() can be retried while connectivity problems persist.
                return;
            }

            try
            {
                var utcNow = DateTimeOffset.UtcNow;
                previousTicks = Interlocked.Read(ref lastReconnectTicks);
                previousReconnectTime = new DateTimeOffset(previousTicks, TimeSpan.Zero);
                elapsedSinceLastReconnect = utcNow - previousReconnectTime;

                if (firstErrorTime == DateTimeOffset.MinValue && !initializing)
                {
                    // We haven't seen an error since last reconnect, so set initial values.
                    firstErrorTime = utcNow;
                    previousErrorTime = utcNow;
                    return;
                }

                if (elapsedSinceLastReconnect < TimeSpan.FromSeconds(cacheConfig.ReconnectMinInterval))
                {
                    return; // Some other thread made it through the check and the lock, so nothing to do.
                }

                TimeSpan elapsedSinceFirstError = utcNow - firstErrorTime;
                TimeSpan elapsedSinceMostRecentError = utcNow - previousErrorTime;

                bool shouldReconnect =
                    elapsedSinceFirstError >= TimeSpan.FromSeconds(cacheConfig.ReconnectErrorThreshold) // Make sure we gave the multiplexer enough time to reconnect on its own if it could.
                    && elapsedSinceMostRecentError <= TimeSpan.FromSeconds(cacheConfig.ReconnectErrorThreshold); // Make sure we aren't working on stale data (e.g. if there was a gap in errors, don't reconnect yet).

                // Update the previousErrorTime timestamp to be now (e.g. this reconnect request).
                previousErrorTime = utcNow;

                if (!shouldReconnect && !initializing)
                {
                    return;
                }

                firstErrorTime = DateTimeOffset.MinValue;
                previousErrorTime = DateTimeOffset.MinValue;

                // Create a new connection
                var _newConnection = await ConnectionMultiplexer.ConnectAsync(cacheConfig.ConnectionString);

                // Swap current connection with the new connection
                var oldConnection = Interlocked.Exchange(ref connectionMultiplexer, _newConnection);

                Interlocked.Exchange(ref lastReconnectTicks, utcNow.UtcTicks);
                IDatabase newDatabase = connectionMultiplexer.GetDatabase();
                Interlocked.Exchange(ref database, newDatabase);

                if (oldConnection != null)
                {
                    try
                    {
                        await oldConnection.CloseAsync();
                    }
                    catch
                    {
                        // Ignore any errors from the old connection
                    }
                }
            }
            finally
            {
                reconnectSemaphore.Release();
            }
        }


        /*
         * Guide to install redis in local
         https://redis.io/docs/latest/operate/oss_and_stack/install/install-redis/install-redis-on-windows/
         */
    }
}
