using Imagekit.Sdk;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Wheelingful.Core.Contracts.Auth;
using Wheelingful.Core.Contracts.Books;
using Wheelingful.Core.Contracts.Images;
using Wheelingful.Core.DTO.Books;
using Wheelingful.Core.Services.Auth;
using Wheelingful.Core.Services.Books;
using Wheelingful.Core.Services.Images;

namespace Wheelingful.Core;

public static class DependencyInjection
{
    public static void AddCoreOptions(this IServiceCollection services, IConfiguration config)
    {
        services.Configure<BookCoverOptions>(options => config.GetSection(nameof(BookCoverOptions)).Bind(options));
    }

    public static void AddCoreServices(this IServiceCollection services)
    {
        services.AddScoped<ICurrentUser, CurrentUser>();

        services.AddScoped<IBookAuthorService, BookAuthorService>();

        services.AddScoped(provider =>
        {
            var config = provider.GetRequiredService<IConfiguration>();

            var publicKey = config.GetRequiredSection("ImageKit:PublicKey").Value;
            var privateKey = config.GetRequiredSection("ImageKit:PrivateKey").Value;
            var endpoint = config.GetRequiredSection("ImageKit:Endpoint").Value;

            return new ImagekitClient(publicKey, privateKey, endpoint);
        });

        services.AddScoped<IImageManager, ImageManager>();
        services.AddScoped<IBookCoverManager, BookCoverManager>();
    }
}
