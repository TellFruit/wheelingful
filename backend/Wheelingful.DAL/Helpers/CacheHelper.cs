using System.Text.Json;

namespace Wheelingful.DAL.Helpers;

public static class CacheHelper
{
    public static TimeSpan DefaultCacheExpiration => TimeSpan.FromMinutes(30);

    public static string GetCacheKey<T>(string prefix, T identity)
    {
        return prefix + JsonSerializer.Serialize(identity);
    }

    public static string ToCachePrefix(this string value, int? id = null)
    {
        if (id.HasValue)
        {
            return $"{value}-{id.Value}:";
        }

        return $"{value}:";
    }
}
