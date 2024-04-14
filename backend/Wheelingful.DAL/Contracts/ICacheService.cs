namespace Wheelingful.DAL.Contracts;

public interface ICacheService
{
    Task<T> GetAndSet<T>(string key, Func<T> fetchValue, TimeSpan expirationTime) where T : class;
    Task<T> GetAndSet<T>(string key, Func<Task<T>> fetchValue, TimeSpan expirationTime) where T : class;
    Task RemoveByKey(string key);
    Task RemoveByPrefix(string prefix);
}
