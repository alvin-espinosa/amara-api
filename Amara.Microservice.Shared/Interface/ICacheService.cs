using Newtonsoft.Json;

namespace Amara.Microservice.Shared.Interface
{
    public interface ICacheService
    {
        Task<T> GetFromCache<T>(
            string key);

        Task SetToCache<T>(
            string key,
            T value,
            int? customExpirationInHours = null,
            JsonSerializerSettings? serializerSettings = null);
    }
}
