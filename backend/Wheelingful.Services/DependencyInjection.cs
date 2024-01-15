using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.JsonWebTokens;
using Wheelingful.Core.Contracts.Auth;
using Wheelingful.Services.Outer.Auth;

namespace Wheelingful.Services;

public static class DependencyInjection
{
    public static void AddServicesCore(this IServiceCollection services)
    {
        services.AddScoped<ITokenService, TokenService>();

        services.AddTransient<JsonWebTokenHandler>();
    }
}
