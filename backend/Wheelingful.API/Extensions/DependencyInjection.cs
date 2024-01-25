using Microsoft.AspNetCore.Authentication;
using Wheelingful.API.Services;
using Wheelingful.Core.Services;
using Wheelingful.Core.Contracts.Auth;

namespace Wheelingful.API.Extensions;

public static class DependencyInjection
{
    public static void AddApiServices(this IServiceCollection services)
    {
        services.AddScoped<IClaimsTransformation, ClaimsTransformation>();
    }
}
