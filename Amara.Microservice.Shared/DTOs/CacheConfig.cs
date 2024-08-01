namespace Amara.Microservice.Shared.DTOs
{
    public class CacheConfig
    {
        public const string Key = "Cache";
        public string ConnectionString { get; set; } = string.Empty;
        public int DefaultExpirationHours { get; set; }
        public int ReconnectErrorThreshold { get; set; }
        public int ReconnectMinInterval { get; set; }
        public int RestartConnectionTimeout { get; set; }
        public int RetryMaxAttempts { get; set; }
    }
}
