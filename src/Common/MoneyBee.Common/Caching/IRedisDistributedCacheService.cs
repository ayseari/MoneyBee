namespace MoneyBee.Common.Caching
{
    public interface IRedisDistributedCacheService
    {
        Task SetAsync<T>(string key, T value);

        Task<T?> GetAsync<T>(string key);

        Task RemoveAsync(string key);
    }
}
