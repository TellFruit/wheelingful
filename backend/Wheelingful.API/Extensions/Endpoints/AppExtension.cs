using SharpGrip.FluentValidation.AutoValidation.Endpoints.Extensions;

namespace Wheelingful.API.Extensions.Endpoints;

public static class AppExtension
{
    public static void MapAppApi(this WebApplication app)
    {
        var bookGroup = app.MapGroup("/books")
            .AddFluentValidationAutoValidation();

        bookGroup.MapBookApi();
        bookGroup.MapChapterApi();

        var userGroup = app.MapGroup("/users")
            .AddFluentValidationAutoValidation();

        userGroup.MapUserApi();
    }
}
