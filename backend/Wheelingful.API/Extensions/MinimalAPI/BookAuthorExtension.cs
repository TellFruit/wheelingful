using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Wheelingful.Core.Contracts.Books;
using Wheelingful.Core.DTO.Books;

namespace Wheelingful.API.Extensions.MinimalAPI;

public static class BookAuthorExtension
{
    public static void MapBookAuthorApi(this IEndpointRouteBuilder endpoints)
    {
        var bookAuthorGroup = endpoints.MapGroup("/bookAuthor").RequireAuthorization("allow_author");

        bookAuthorGroup.MapPost("/createBook", async Task<Results<Created, ValidationProblem, ForbidHttpResult>>
            ([FromBody] NewBookModel newBook, [FromServices] IBookAuthorService ba) =>
        {
            await ba.CreateBook(newBook);

            return TypedResults.Created();
        });

        bookAuthorGroup.MapPost("/updateBook", async Task<Results<Ok, ValidationProblem, ForbidHttpResult>>
            ([FromBody] UpdatedBookModel updatedBook, [FromServices] IBookAuthorService ba) =>
        {
            await ba.UpdateBook(updatedBook);

            return TypedResults.Ok();
        });

        bookAuthorGroup.MapPost("/deleteBook", async Task<Results<NotFound, ValidationProblem, ForbidHttpResult>>
            ([FromBody] int bookId, [FromServices] IBookAuthorService ba) =>
        {
            await ba.DeleteBook(bookId);

            return TypedResults.NotFound();
        });
    }
}
