namespace Repository.Cache;
public interface ICacheRepository
{
    Task AddAsync<T>(string key, T data, TimeSpan? absoluteExpirationTime = null, TimeSpan? slidingExpiration = null);
    Task<T> GetAsync<T>(string key);
    Task RemoveAsync(string key);
}