using Wheelingful.DAL.Contracts;

namespace Wheelingful.DAL.Services;

public class FakeCacheService : ICacheService
{
    public async Task<T> GetAndSet<T>(string key, Func<Task<T>> fetchData, TimeSpan expirationTime) where T : class
    {
        return await fetchData();
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
