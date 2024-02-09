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
        var booksGroup = endpoints.MapGroup("/books")
            .AddFluentValidationAutoValidation();

        booksGroup.MapGet("/{bookId}/chapters", 
            async Task<Results<Ok<FetchChapterPaginationResponse>, ValidationProblem>> (
                [AsParameters] FetchChapterPaginationRequest request, [FromServices] IChapterReaderService handler) =>
        {
            var result = await handler.GetChapters(request);

            return TypedResults.Ok(result);
        });

        booksGroup.MapGet("/chapters/{chapterId}", 
            async Task<Results<Ok<FetchChapterResponse>, ValidationProblem>> (
                [AsParameters] FetchChapterRequest request, [FromServices] IChapterReaderService handler) =>
       {
           var result = await handler.GetChapter(request);

           return TypedResults.Ok(result);
       });
    }
}
