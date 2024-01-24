using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Wheelingful.API.Constants;
using Wheelingful.Core.Contracts.Books;
using Wheelingful.Core.DTO.Books;

namespace Wheelingful.API.Extensions.MinimalAPI;

public static class BookAuthorExtension
{
    public static void MapBookAuthorApi(this IEndpointRouteBuilder endpoints)
    {
        var bookAuthorGroup = endpoints.MapGroup("/book-author").RequireAuthorization(PolicyContants.AuthorizeAuthor);

        bookAuthorGroup.MapPost("/books", async Task<Results<Created, ValidationProblem>>
            ([FromBody] NewBookModel model, [FromServices] IBookAuthorService ba) =>
        {
            await ba.CreateBook(model);

            return TypedResults.Created();
        });

        bookAuthorGroup.MapPut("/books", async Task<Results<Ok, ValidationProblem>>
            ([FromBody] UpdatedBookModel model, [FromServices] IBookAuthorService ba) =>
        {
            await ba.UpdateBook(model);

            return TypedResults.Ok();
        });

        bookAuthorGroup.MapDelete("/books/{bookId}", async Task<Results<NoContent, ValidationProblem>>
            ([FromRoute] int bookId, [FromServices] IBookAuthorService ba) =>
        {
            await ba.DeleteBook(bookId);

            return TypedResults.NoContent();
        });
    }
}
