using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SharpGrip.FluentValidation.AutoValidation.Endpoints.Extensions;
using Wheelingful.API.Constants;
using Wheelingful.BLL.Contracts.Chapters;
using Wheelingful.BLL.Models.Requests;

namespace Wheelingful.API.Extensions.Endpoints;

public static class ChapterAuthorExtension
{
    public static void MapChapterAuthorApi(this IEndpointRouteBuilder endpoints)
    {
        var chapterAuthorGroup = endpoints.MapGroup("/chapter-author")
            .RequireAuthorization(PolicyContants.AuthorizeAuthor)
            .AddFluentValidationAutoValidation();

        chapterAuthorGroup.MapPost("/", async Task<Results<Created, ValidationProblem>> (
             [FromBody] CreateChapterRequest request, [FromServices] IChapterAuthorService handler) =>
        {
            await handler.CreateChapter(request);

            return TypedResults.Created();
        });

        chapterAuthorGroup.MapPut("/props", async Task<Results<Ok, ValidationProblem>> (
             [FromBody] UpdateChapterPropertiesRequest request, [FromServices] IChapterAuthorService handler) =>
        {
            await handler.UpdateChapterProperties(request);

            return TypedResults.Ok();
        });

        chapterAuthorGroup.MapPut("/text", async Task<Results<Ok, ValidationProblem>> (
             [FromBody] UpdateChapterTextRequest request, [FromServices] IChapterAuthorService handler) =>
        {
            await handler.UpdateChapterText(request);

            return TypedResults.Ok();
        });

        chapterAuthorGroup.MapDelete("/book/{bookId}/chapter/{chapterId}", async Task<Results<NoContent, ValidationProblem>> (
             [AsParameters] DeleteChapterRequest request, [FromServices] IChapterAuthorService handler) =>
        {
            await handler.DeleteChapter(request);

            return TypedResults.NoContent();
        });
    }
}