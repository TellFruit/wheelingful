using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Wheelingful.DAL.DbContexts;

namespace Wheelingful.DAL;

public static class DependencyInjection
{
    public static void AddDbContext(this IServiceCollection services, IConfiguration config)
    {
        var serverVersion = new MySqlServerVersion(new Version(5, 7));

        services.AddDbContext<WheelingfulDbContext>(options =>
            options.UseMySql(config.GetConnectionString("DefaultConnection"), serverVersion));
    }
}
