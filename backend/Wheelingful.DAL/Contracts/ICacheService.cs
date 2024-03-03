namespace Wheelingful.DAL.Contracts;

public interface ICacheService
{
    Task<T> GetAndSet<T>(string key, Func<Task<T>> fetchValue, TimeSpan expirationTime) where T : class;
    Task RemoveByPrefix(string prefix);
}
