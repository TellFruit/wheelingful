using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Wheelingful.Core.DTO.Books;

namespace Wheelingful.Core;

public static class DependencyInjection
{
    public static void AddCoreOptions(this IServiceCollection services, IConfiguration config)
    {
        services.Configure<BookCoverOptions>(options => config.GetSection(nameof(BookCoverOptions)).Bind(options));
    }
}
