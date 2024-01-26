using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SharpGrip.FluentValidation.AutoValidation.Endpoints.Extensions;
using Wheelingful.BLL.Contracts.Books;
using Wheelingful.BLL.Contracts.Generic;
using Wheelingful.BLL.Models.Requests;
using Wheelingful.BLL.Models.Requests.General;
using Wheelingful.BLL.Models.Responses;
using Wheelingful.DAL.Entities;

namespace Wheelingful.API.Extensions.Endpoints;

public static class BookReaderExtension
{
    public static void MapBookReaderApi(this IEndpointRouteBuilder endpoints)
    {
        var bookReaderGroup = endpoints.MapGroup("/book-reader")
            .AddFluentValidationAutoValidation();

        bookReaderGroup.MapGet("/pagination", async Task<Results<Ok<List<FetchBookResponse>>, ValidationProblem>>
            ([AsParameters] FetchPaginationRequest model, [FromServices] IBookReaderService handler) =>
        {
            var result = await handler.GetBooks(model);

            return TypedResults.Ok(result);
        });

        bookReaderGroup.MapGet("/pages", async Task<Results<Ok<int>, ValidationProblem>>
            ([AsParameters] CountPagesRequest request, [FromServices] ICountPaginationPages<Book> handler) =>
        {
            var result = await handler.CountByPageSize(request);

            return TypedResults.Ok(result);
        });

        bookReaderGroup.MapGet("/", async Task<Results<Ok<FetchBookResponse>, ValidationProblem>>
            ([AsParameters] FetchBookRequest model, [FromServices] IBookReaderService handler) =>
        {
            var result = await handler.GetBook(model);

            return TypedResults.Ok(result);
        });
    }
}
