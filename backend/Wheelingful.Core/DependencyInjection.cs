using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Wheelingful.Core.DTO.Options;

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
            options.HoursUntilExpired = int.Parse(jwtAppOptions[nameof(JwtOptions.HoursUntilExpired)]!);
            options.SecretKey = jwtAppOptions[nameof(JwtOptions.SecretKey)]!;
        });
    }
}
