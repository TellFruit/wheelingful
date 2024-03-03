using Microsoft.Extensions.Caching.Distributed;
using StackExchange.Redis;
using System.Text.Json;
using Wheelingful.DAL.Contracts;

namespace Wheelingful.DAL.Services;

public class RedisCacheService(
    IDistributedCache distributedCache, 
    IConnectionMultiplexer redis) : ICacheService
{
    private readonly IDatabase _redisDb = redis.GetDatabase();

    public async Task<T> GetAndSet<T>(string key, Func<Task<T>> fetchData, TimeSpan expirationTime)
        where T : class
    {
        var cache = await distributedCache.GetAsync(key);

        if (cache != null)
        {
            return JsonSerializer.Deserialize<T>(cache)!;
        }

        var data = await fetchData();

        var newCache = JsonSerializer.SerializeToUtf8Bytes(data);

        var options = new DistributedCacheEntryOptions
        {
            AbsoluteExpiration = DateTime.UtcNow.Add(expirationTime),
            SlidingExpiration = expirationTime,
        };

        await distributedCache.SetAsync(key, newCache, options);

        return data;
    }

    // TODO: Redesign to avoid low performance risk and dependency on Redis
    public async Task RemoveByPrefix(string prefix)
    {
        var enpoint = redis.GetEndPoints().First();

        var server = redis.GetServer(enpoint);

        var keys = server.Keys(pattern: prefix + "*");

        await _redisDb.KeyDeleteAsync(keys.ToArray());
    }
}
