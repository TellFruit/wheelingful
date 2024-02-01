using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SharpGrip.FluentValidation.AutoValidation.Endpoints.Extensions;
using Wheelingful.BLL.Contracts.Chapters;
using Wheelingful.BLL.Models.Requests;
using Wheelingful.BLL.Models.Responses;

namespace Wheelingful.API.Extensions.Endpoints;

public static class ChapterReaderExtension
{
    public static void MapChapterReaderApi(this IEndpointRouteBuilder endpoints)
    {
        var chapterReaderGroup = endpoints.MapGroup("/chapter-reader")
            .AddFluentValidationAutoValidation();

        chapterReaderGroup.MapGet("/book/{bookId}", 
            async Task<Results<Ok<List<FetchChapterPaginatedResponse>>, ValidationProblem>> (
                [AsParameters] FetchChapterPaginationRequest request, [FromServices] IChapterReaderService handler) =>
        {
            var result = await handler.GetChapters(request);

            return TypedResults.Ok(result);
        });

        chapterReaderGroup.MapGet("/pages/book/{bookId}", async Task<Results<Ok<int>, ValidationProblem>> (
            [AsParameters] CountChapterPaginationPagesRequest request, [FromServices] IChapterReaderService handler) =>
        {
            var result = await handler.CountPaginationPages(request);

            return TypedResults.Ok(result);
        });

        chapterReaderGroup.MapGet("/book/{bookId}/chapter/{chapterId}", 
            async Task<Results<Ok<FetchChapterResponse>, ValidationProblem>> (
                [AsParameters] FetchChapterRequest request, [FromServices] IChapterReaderService handler) =>
       {
           var result = await handler.GetChapter(request);

           return TypedResults.Ok(result);
       });
    }
}
