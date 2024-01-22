using Microsoft.Extensions.DependencyInjection;
using Wheelingful.Core.Contracts.Books;
using Wheelingful.Services.Outer;

namespace Wheelingful.Services;

public static class DependencyInjection
{
    public static void AddServicesOuter(this IServiceCollection services)
    {
        services.AddScoped<IBookAuthorService, BookAuthorService>();
    }
}
