using SharpGrip.FluentValidation.AutoValidation.Endpoints.Extensions;

namespace Wheelingful.API.Extensions.Endpoints;

public static class GroupExtension
{
    public static void MapApiGroups(this WebApplication app)
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
