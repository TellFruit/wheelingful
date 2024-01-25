using Microsoft.AspNetCore.Authentication;
using Wheelingful.API.Services;
using Wheelingful.BLL.Services;
using Wheelingful.BLL.Contracts.Auth;

namespace Wheelingful.API.Extensions;

public static class DependencyInjection
{
    public static void AddApiServices(this IServiceCollection services)
    {
        services.AddScoped<IClaimsTransformation, ClaimsTransformation>();
    }
}
