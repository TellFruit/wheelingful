using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Wheelingful.Core.Contracts.Books;
using Wheelingful.Core.DTO.Books;

namespace Wheelingful.API.Extensions.MinimalAPI;

public static class BookAuthorExtension
{
    public static void MapBookAuthorApi(this IEndpointRouteBuilder endpoints)
    {
        var bookAuthorGroup = endpoints.MapGroup("/bookAuthor")/*.RequireAuthorization("allow_author")*/;

        bookAuthorGroup.MapPost("/createBook", async Task<Results<Created, ValidationProblem, ForbidHttpResult>>
            ([FromBody] NewBookModel model, [FromServices] IBookAuthorService ba) =>
        {
            await ba.CreateBook(model);

            return TypedResults.Created();
        });

        bookAuthorGroup.MapPut("/updateBook", async Task<Results<Ok, ValidationProblem, ForbidHttpResult>>
            ([FromBody] UpdatedBookModel model, [FromServices] IBookAuthorService ba) =>
        {
            await ba.UpdateBook(model);

            return TypedResults.Ok();
        });

        bookAuthorGroup.MapDelete("/deleteBook/{bookId}", async Task<Results<NotFound, ValidationProblem, ForbidHttpResult>>
            ([FromRoute] int bookId, [FromServices] IBookAuthorService ba) =>
        {
            await ba.DeleteBook(bookId);

            return TypedResults.NotFound();
        });
    }
}
