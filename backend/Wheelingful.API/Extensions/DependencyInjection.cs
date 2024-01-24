using Microsoft.AspNetCore.Authentication;
using Wheelingful.API.Services;
using Wheelingful.API.Services.Outer;
using Wheelingful.Core.Contracts.Auth;

namespace Wheelingful.API.Extensions;

public static class DependencyInjection
{
    public static void AddApiOuter(this IServiceCollection services)
    {
        services.AddScoped<ICurrentUser, CurrentUser>();
    }

    public static void AddApiInternalServices(this IServiceCollection services)
    {
        services.AddScoped<IClaimsTransformation, ClaimsTransformation>();
    }
}
