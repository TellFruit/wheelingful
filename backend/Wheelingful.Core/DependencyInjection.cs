using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Wheelingful.Core.DTO.Auth;

namespace Wheelingful.Core;

public static class DependencyInjection
{
    public static void AddOptions(this IServiceCollection services, IConfiguration config)
    {
        var jwtAppOptions = config.GetSection(nameof(JwtOptions));

        services.Configure<JwtOptions>(options =>
        {
            options.Issuer = jwtAppOptions[nameof(JwtOptions.Issuer)]!;
            options.Audience = jwtAppOptions[nameof(JwtOptions.Audience)]!;
            options.HoursUntilAccessExpired = int.Parse(jwtAppOptions["HoursUntil:AcessExpired"]!);
            options.HoursUntilRefershExpired = int.Parse(jwtAppOptions["HoursUntil:RefreshExpired"]!);
            options.SecretKey = jwtAppOptions[nameof(JwtOptions.SecretKey)]!;
        });
    }
}
