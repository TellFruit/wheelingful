using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Wheelingful.BLL.Contracts.Books;
using Wheelingful.BLL.Contracts.Generic;
using Wheelingful.BLL.Models.Books;
using Wheelingful.BLL.Models.General;
using Wheelingful.DAL.Entities;

namespace Wheelingful.API.Extensions.Endpoints;

public static class BookReaderExtension
{
    public static void MapBookReaderApi(this IEndpointRouteBuilder endpoints)
    {
        var bookReaderGroup = endpoints.MapGroup("/book-reader");

        bookReaderGroup.MapGet("/", async Task<Results<Ok<List<FetchBookModel>>, ValidationProblem>>
            ([FromQuery] int pageNumber, [FromQuery] int pageSize, [FromServices] IBookReaderService handler) =>
        {
            var result = await handler.GetBooks(new FetchRequest
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
            });

            return TypedResults.Ok(result);
        });

        bookReaderGroup.MapGet("/{bookId}", async Task<Results<Ok<FetchBookModel>, ValidationProblem>>
            ([FromRoute] int bookId, [FromServices] IBookReaderService handler) =>
        {
            var result = await handler.GetBook(bookId);

            return TypedResults.Ok(result);
        });

        bookReaderGroup.MapGet("/pages", async Task<Results<Ok<int>, ValidationProblem>>
            ([FromQuery] int pageSize, [FromServices] ICountPaginationPages<Book> handler) =>
        {
            var result = await handler.CountByPageSize(pageSize);

            return TypedResults.Ok(result);
        });
    }
}
