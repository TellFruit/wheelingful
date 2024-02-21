using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Text.RegularExpressions;
using Wheelingful.DAL.DbContexts;

namespace Wheelingful.DAL;

public static class DependencyInjection
{
    public static void AddDbContext(this IServiceCollection services, IConfiguration config)
    {
        var serverVersion = new MySqlServerVersion(new Version(5, 7));

        var initialConnection = Environment.GetEnvironmentVariable("MYSQLCONNSTR_localdb")!;
        var dbhost = Regex.Match(initialConnection, @"Data Source=(.+?);").Groups[1].Value;
        var server = dbhost.Split(':')[0].ToString();
        var port = dbhost.Split(':')[1].ToString();
        var dbname = Regex.Match(initialConnection, @"Database=(.+?);").Groups[1].Value;
        var dbusername = Regex.Match(initialConnection, @"User Id=(.+?);").Groups[1].Value;
        var dbpassword = Regex.Match(initialConnection, @"Password=(.+?)$").Groups[1].Value;

        string parsedConnection = $@"server={server};port={port};database={dbname};user={dbusername};password={dbpassword};";

        services.AddDbContext<WheelingfulDbContext>(options =>
            options.UseMySql(parsedConnection, serverVersion));
    }
}
