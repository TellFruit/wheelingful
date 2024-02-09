using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SharpGrip.FluentValidation.AutoValidation.Endpoints.Extensions;
using Wheelingful.BLL.Contracts.Books;
using Wheelingful.BLL.Models.Requests;
using Wheelingful.BLL.Models.Responses;

namespace Wheelingful.API.Extensions.Endpoints;

public static class BookReaderExtension
{
    public static void MapBookReaderApi(this IEndpointRouteBuilder endpoints)
    {
        var booksGroup = endpoints.MapGroup("/books")
            .AddFluentValidationAutoValidation();

        booksGroup.MapGet("/", async Task<Results<Ok<FetchBookPaginationResponse>, ValidationProblem>>
            ([AsParameters] FetchBookPaginationRequest request, [FromServices] IBookReaderService handler) =>
        {
            var result = await handler.GetBooks(request);

            return TypedResults.Ok(result);
        });

        booksGroup.MapGet("/{bookId}", async Task<Results<Ok<FetchBookResponse>, ValidationProblem>>
            ([AsParameters] FetchBookRequest request, [FromServices] IBookReaderService handler) =>
        {
            var result = await handler.GetBook(request);

            return TypedResults.Ok(result);
        });
    }
}
