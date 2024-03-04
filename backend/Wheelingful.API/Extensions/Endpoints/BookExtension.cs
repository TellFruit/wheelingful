using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Wheelingful.API.Constants;
using Wheelingful.API.Models.Bindings;
using Wheelingful.BLL.Contracts.Books;
using Wheelingful.BLL.Models.Requests;
using Wheelingful.BLL.Models.Responses;
using Wheelingful.BLL.Models.Responses.Generic;

namespace Wheelingful.API.Extensions.Endpoints;

public static class BookExtension
{
    public static void MapBookApi(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost("/", async Task<Results<Created, ValidationProblem>>(
             [FromBody] CreateBookRequest request, [FromServices] IBookAuthorService handler) =>
        {
            await handler.CreateBook(request);

            return TypedResults.Created();
        })
            .RequireAuthorization(PolicyContants.AuthorizeAuthor);

        endpoints.MapPut("/{bookId}", async Task<Results<Ok, ValidationProblem>>
            ([AsParameters] UpdateBookBinding request, [FromServices] IBookAuthorService handler) =>
        {
            await handler.UpdateBook(request.To());

            return TypedResults.Ok();
        })
            .RequireAuthorization(PolicyContants.AuthorizeAuthor);

        endpoints.MapDelete("/{bookId}", async Task<Results<NoContent, ValidationProblem>>
            ([AsParameters] DeleteBookRequest request, [FromServices] IBookAuthorService handler) =>
        {
            await handler.DeleteBook(request);

            return TypedResults.NoContent();
        })
            .RequireAuthorization(PolicyContants.AuthorizeAuthor);

        endpoints.MapGet("/", async Task<Results<Ok<FetchPaginationResponse<FetchBookResponse>>, ValidationProblem>>
            ([AsParameters] FetchBookPaginationRequest request, [FromServices] IBookReaderService handler) =>
        {
            var result = await handler.GetBooks(request);

            return TypedResults.Ok(result);
        });

        endpoints.MapGet("/{bookId}", async Task<Results<Ok<FetchBookResponse>, ValidationProblem>>
            ([AsParameters] FetchBookRequest request, [FromServices] IBookReaderService handler) =>
        {
            var result = await handler.GetBook(request);

            return TypedResults.Ok(result);
        });
    }
}
