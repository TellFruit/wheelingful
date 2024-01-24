using Imagekit.Sdk;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Wheelingful.Core.Contracts.Books;
using Wheelingful.Core.Contracts.Images;
using Wheelingful.Services.Outer;

namespace Wheelingful.Services;

public static class DependencyInjection
{
    public static void AddServicesOuter(this IServiceCollection services)
    {
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
