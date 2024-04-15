using Wheelingful.DAL.Contracts;

namespace Wheelingful.DAL.Services;

public class FakeCacheService : ICacheService
{
    public Task<T> GetAndSet<T>(string key, Func<Task<T>> fetchData, TimeSpan expirationTime) where T : class
    {
        return fetchData();
    }

    public Task<T> GetAndSet<T>(string key, Func<T> fetchValue, TimeSpan expirationTime) where T : class
    {
        return Task.FromResult(fetchValue());
    }

    public Task RemoveByKey(string key)
    {
        return Task.CompletedTask;
    }

    public Task RemoveByPrefix(string prefix)
    {
        return Task.CompletedTask;
    }
}
