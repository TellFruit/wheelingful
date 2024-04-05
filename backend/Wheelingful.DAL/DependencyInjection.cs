using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using System.Text.RegularExpressions;
using Wheelingful.DAL.Contracts;
using Wheelingful.DAL.DbContexts;
using Wheelingful.DAL.Services;

namespace Wheelingful.DAL;

public static class DependencyInjection
{
    public static void AddDbContext(this IServiceCollection services)
    {
        var serverVersion = new MySqlServerVersion(new Version(5, 7));

        var initialConnection = Environment.GetEnvironmentVariable("MYSQLCONNSTR_localdb")!;

        string parsedConnection;
        if (initialConnection.Contains("Data Source"))
        {
            var dbhost = Regex.Match(initialConnection, @"Data Source=(.+?);").Groups[1].Value;
            var server = dbhost.Split(':')[0].ToString();
            var port = dbhost.Split(':')[1].ToString();
            var dbname = Regex.Match(initialConnection, @"Database=(.+?);").Groups[1].Value;
            var dbusername = Regex.Match(initialConnection, @"User Id=(.+?);").Groups[1].Value;
            var dbpassword = Regex.Match(initialConnection, @"Password=(.+?)$").Groups[1].Value;

            parsedConnection = $@"server={server};port={port};database={dbname};user={dbusername};password={dbpassword};";
        }
        else
        {
            parsedConnection = initialConnection;
        }

        services.AddDbContext<WheelingfulDbContext>(options =>
            options.UseMySql(parsedConnection, serverVersion));
    }

    public static void AddCacheService(this IServiceCollection services, IConfiguration config)
    {
        var redisConnection = config.GetConnectionString("RedisConnection");

        if (string.IsNullOrWhiteSpace(redisConnection))
        {
            services.AddScoped<ICacheService, FakeCacheService>();
        }
        else
        {
            services.AddStackExchangeRedisCache(options => options.Configuration = redisConnection);

            services.AddSingleton<IConnectionMultiplexer>(
                ConnectionMultiplexer
                      .Connect(redisConnection!));

            services.AddScoped<ICacheService, RedisCacheService>();
        }
    }
}
