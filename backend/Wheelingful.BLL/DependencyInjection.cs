using Imagekit.Sdk;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Wheelingful.BLL.Contracts.Auth;
using Wheelingful.BLL.Contracts.Books;
using Wheelingful.BLL.Contracts.Chapters;
using Wheelingful.BLL.Contracts.Images;
using Wheelingful.BLL.Models.Options;
using Wheelingful.BLL.Services.Auth;
using Wheelingful.BLL.Services.Books;
using Wheelingful.BLL.Services.Chapters;
using Wheelingful.BLL.Services.Images;

namespace Wheelingful.BLL;

public static class DependencyInjection
{
    public static void AddCoreOptions(this IServiceCollection services, IConfiguration config)
    {
        services.Configure<BookCoverOptions>(options => 
            config.GetSection(nameof(BookCoverOptions)).Bind(options));
    }

    public static void AddCoreServices(this IServiceCollection services)
    {
        services.AddScoped<ICurrentUser, CurrentUser>();

        services.AddScoped<IBookAuthorService, BookAuthorService>();
        services.AddScoped<IBookCoverService, BookCoverService>();
        services.AddScoped<IBookReaderService, BookReaderService>();
        services.AddScoped<IReviewService, ReviewService>();
        services.AddScoped<IBookRecommenderService, BookRecommenderService>();

        services.AddScoped<IChapterAuthorService, ChapterAuthorService>();
        services.AddScoped<IChapterTextService, ChapterTextService>();
        services.AddScoped<IChapterReaderService,  ChapterReaderService>();

        services.AddScoped(provider =>
        {
            var config = provider.GetRequiredService<IConfiguration>();

            var publicKey = config.GetRequiredSection("ImageKit:PublicKey").Value;
            var privateKey = config.GetRequiredSection("ImageKit:PrivateKey").Value;
            var endpoint = config.GetRequiredSection("ImageKit:Endpoint").Value;

            return new ImagekitClient(publicKey, privateKey, endpoint);
        });
        services.AddScoped<IImageService, ImageService>();

        foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(assembly));
        }
    }
}
