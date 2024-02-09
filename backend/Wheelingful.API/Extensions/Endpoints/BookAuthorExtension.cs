using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SharpGrip.FluentValidation.AutoValidation.Endpoints.Extensions;
using Wheelingful.API.Constants;
using Wheelingful.API.Models.Bindings;
using Wheelingful.BLL.Contracts.Books;
using Wheelingful.BLL.Models.Requests;

namespace Wheelingful.API.Extensions.Endpoints;

public static class BookAuthorExtension
{
    public static void MapBookAuthorApi(this IEndpointRouteBuilder endpoints)
    {
        var booksGroup = endpoints.MapGroup("/books")
            .RequireAuthorization(PolicyContants.AuthorizeAuthor)
            .AddFluentValidationAutoValidation();

        booksGroup.MapPost("/", async Task<Results<Created, ValidationProblem>>(
             [FromBody] CreateBookRequest request, [FromServices] IBookAuthorService handler) =>
        {
            await handler.CreateBook(request);

            return TypedResults.Created();
        });

        booksGroup.MapPut("/{bookId}", async Task<Results<Ok, ValidationProblem>>
            ([AsParameters] UpdateBookBinding request, [FromServices] IBookAuthorService handler) =>
        {
            await handler.UpdateBook(request.To());

            return TypedResults.Ok();
        });

        booksGroup.MapDelete("/{bookId}", async Task<Results<NoContent, ValidationProblem>>
            ([AsParameters] DeleteBookRequest request, [FromServices] IBookAuthorService handler) =>
        {
            await handler.DeleteBook(request);

            return TypedResults.NoContent();
        });
    }
}
