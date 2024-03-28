using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Testcontainers.MySql;
using Testcontainers.Redis;
using Wheelingful.BLL.Contracts.Auth;
using Wheelingful.DAL.DbContexts;
using Wheelingful.IntegrationTests.Constants;
using Wheelingful.IntegrationTests.Mocks;

namespace Wheelingful.IntegrationTests.Abstract;

public abstract class BaseTest : IAsyncLifetime
{
    private readonly MySqlContainer _mySqlContainer = new MySqlBuilder().Build();
    private readonly RedisContainer _redisContainer = new RedisBuilder().Build();

    protected WebApplicationFactory<Program> Factory { get; private set; } = null!;
    protected HttpClient HttpClient { get; private set; } = null!;

    public async Task InitializeAsync()
    {
        await _mySqlContainer.StartAsync();
        await _redisContainer.StartAsync();

        Environment.SetEnvironmentVariable("MYSQLCONNSTR_localdb", _mySqlContainer.GetConnectionString());

        Factory = new WebApplicationFactory<Program>()
            .WithWebHostBuilder(b =>
            {
                b.UseEnvironment(ApiConstants.BaseEnvironment);
                b.ConfigureServices(s =>
                {
                    RemoveService<ICurrentUser>(s);

                    s.AddScoped<ICurrentUser, MockCurrentUser>();
                });
                b.UseSetting("ConnectionStrings:RedisConnection", _redisContainer.GetConnectionString());
            });

        HttpClient = Factory.CreateClient(new WebApplicationFactoryClientOptions
        {
            BaseAddress = new Uri(ApiConstants.BaseUrl),
        });

        using var scope = Factory.Services.CreateScope();
        await using var db = scope.ServiceProvider.GetRequiredService<WheelingfulDbContext>();
        await db.Database.MigrateAsync();
    }

    public async Task DisposeAsync()
    {
        HttpClient.Dispose();
        await Factory.DisposeAsync();
        await _mySqlContainer.DisposeAsync();
        await _redisContainer.DisposeAsync();
    }

    private void RemoveService<T>(IServiceCollection services)
    {
        services.Where(s => s.ServiceType == typeof(T)).ToList().Select(services.Remove);
    }
}
