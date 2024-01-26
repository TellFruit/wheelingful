using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Wheelingful.API.Constants;
using Wheelingful.BLL.Contracts.Books;
using Wheelingful.BLL.Models.Books;

namespace Wheelingful.API.Extensions.Endpoints;

public static class BookAuthorExtension
{
    public static void MapBookAuthorApi(this IEndpointRouteBuilder endpoints)
    {
        var bookAuthorGroup = endpoints.MapGroup("/book-author").RequireAuthorization(PolicyContants.AuthorizeAuthor);

        bookAuthorGroup.MapPost("/books", async Task<Results<Created, ValidationProblem>>
            ([FromBody] NewBookModel model, [FromServices] IBookAuthorService handler) =>
        {
            try
            {
                await handler.CreateBook(model);
            }
            catch (Exception error)
            {
                return CreateValidationProblem(error);
            }

            return TypedResults.Created();
        });

        bookAuthorGroup.MapPut("/books", async Task<Results<Ok, ValidationProblem>>
            ([FromBody] UpdatedBookModel model, [FromServices] IBookAuthorService handler) =>
        {
            try
            {
                await handler.UpdateBook(model);
            }
            catch (Exception error)
            {
                return CreateValidationProblem(error);
            }

            return TypedResults.Ok();
        });

        bookAuthorGroup.MapDelete("/books/{bookId}", async Task<Results<NoContent, ValidationProblem>>
            ([FromRoute] int bookId, [FromServices] IBookAuthorService handler) =>
        {
            try
            {
                await handler.DeleteBook(bookId);
            }
            catch (Exception error)
            {
                return CreateValidationProblem(error);
            }

            return TypedResults.NoContent();
        });
    }

    private static ValidationProblem CreateValidationProblem(Exception error) =>
        TypedResults.ValidationProblem(new Dictionary<string, string[]> {
            { nameof(error), [error.Message] }
        });
}
