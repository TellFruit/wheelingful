using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SharpGrip.FluentValidation.AutoValidation.Endpoints.Extensions;
using Wheelingful.API.Constants;
using Wheelingful.BLL.Contracts.Books;
using Wheelingful.BLL.Models.Requests;

namespace Wheelingful.API.Extensions.Endpoints;

public static class BookAuthorExtension
{
    public static void MapBookAuthorApi(this IEndpointRouteBuilder endpoints)
    {
        var bookAuthorGroup = endpoints.MapGroup("/book-author")
            .RequireAuthorization(PolicyContants.AuthorizeAuthor)
            .AddFluentValidationAutoValidation();

        bookAuthorGroup.MapPost("/", async Task<Results<Created, ValidationProblem>>(
             [FromBody] CreateBookRequest model, [FromServices] IBookAuthorService handler) =>
        {
            await handler.CreateBook(model);

            return TypedResults.Created();
        });

        bookAuthorGroup.MapPut("/", async Task<Results<Ok, ValidationProblem>>
            ([FromBody] UpdateBookRequest model, [FromServices] IBookAuthorService handler) =>
        {
            await handler.UpdateBook(model);

            return TypedResults.Ok();
        });

        bookAuthorGroup.MapDelete("/{bookId}", async Task<Results<NoContent, ValidationProblem>>
            ([AsParameters] DeleteBookRequest model, [FromServices] IBookAuthorService handler) =>
        {
            await handler.DeleteBook(model);

            return TypedResults.NoContent();
        });
    }
}
