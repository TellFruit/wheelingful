using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SharpGrip.FluentValidation.AutoValidation.Endpoints.Extensions;
using Wheelingful.API.Constants;
using Wheelingful.API.Models.Bindings;
using Wheelingful.BLL.Contracts.Chapters;
using Wheelingful.BLL.Models.Requests;

namespace Wheelingful.API.Extensions.Endpoints;

public static class ChapterAuthorExtension
{
    public static void MapChapterAuthorApi(this IEndpointRouteBuilder endpoints)
    {
        var booksGroup = endpoints.MapGroup("/books")
            .RequireAuthorization(PolicyContants.AuthorizeAuthor)
            .AddFluentValidationAutoValidation();

        booksGroup.MapPost("/chapters", async Task<Results<Created, ValidationProblem>> (
             [FromBody] CreateChapterRequest request, [FromServices] IChapterAuthorService handler) =>
        {
            await handler.CreateChapter(request);

            return TypedResults.Created();
        });

        booksGroup.MapPut("/{bookId}/chapters/{chapterId}", async Task<Results<Ok, ValidationProblem>> (
             [AsParameters] UpdateChapterBinding request, [FromServices] IChapterAuthorService handler) =>
        {
            await handler.UpdateChapter(request.To());

            return TypedResults.Ok();
        });

        booksGroup.MapDelete("/{bookId}/chapters/{chapterId}", async Task<Results<NoContent, ValidationProblem>> (
             [AsParameters] DeleteChapterRequest request, [FromServices] IChapterAuthorService handler) =>
        {
            await handler.DeleteChapter(request);

            return TypedResults.NoContent();
        });
    }
}