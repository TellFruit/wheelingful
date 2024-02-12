using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Wheelingful.API.Constants;
using Wheelingful.API.Models.Bindings;
using Wheelingful.BLL.Contracts.Chapters;
using Wheelingful.BLL.Models.Requests;
using Wheelingful.BLL.Models.Responses;

namespace Wheelingful.API.Extensions.Endpoints;

public static class ChapterExtension
{
    public static void MapChapterApi(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost("/{bookId}/chapters", async Task<Results<Created, ValidationProblem>> (
             [AsParameters] CreateChapterBinding request, [FromServices] IChapterAuthorService handler) =>
        {
            await handler.CreateChapter(request.To());

            return TypedResults.Created();
        })
            .RequireAuthorization(PolicyContants.AuthorizeAuthor);

        endpoints.MapPut("/{bookId}/chapters/{chapterId}", async Task<Results<Ok, ValidationProblem>> (
             [AsParameters] UpdateChapterBinding request, [FromServices] IChapterAuthorService handler) =>
        {
            await handler.UpdateChapter(request.To());

            return TypedResults.Ok();
        })
            .RequireAuthorization(PolicyContants.AuthorizeAuthor);

        endpoints.MapDelete("/{bookId}/chapters/{chapterId}", async Task<Results<NoContent, ValidationProblem>> (
             [AsParameters] DeleteChapterRequest request, [FromServices] IChapterAuthorService handler) =>
        {
            await handler.DeleteChapter(request);

            return TypedResults.NoContent();
        })
            .RequireAuthorization(PolicyContants.AuthorizeAuthor);

        endpoints.MapGet("/{bookId}/chapters",
            async Task<Results<Ok<FetchChapterPaginationResponse>, ValidationProblem>> (
                [AsParameters] FetchChapterPaginationRequest request, [FromServices] IChapterReaderService handler) =>
            {
                var result = await handler.GetChapters(request);

                return TypedResults.Ok(result);
            });

        endpoints.MapGet("/chapters/{chapterId}",
            async Task<Results<Ok<FetchChapterResponse>, ValidationProblem>> (
                [AsParameters] FetchChapterRequest request, [FromServices] IChapterReaderService handler) =>
            {
                var result = await handler.GetChapter(request);

                return TypedResults.Ok(result);
            });
    }
}