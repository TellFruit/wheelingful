using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
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
        var bookReaderGroup = endpoints.MapGroup("/book-reader");

        bookReaderGroup.MapGet("/", async Task<Results<Ok<List<FetchBookResponse>>, ValidationProblem>>
            ([FromQuery] int? pageNumber,
             [FromQuery] int? pageSize,
             [FromServices] IValidator<FetchPaginationRequest> validator,
             [FromServices] IBookReaderService handler) =>
        {
            var request = new FetchPaginationRequest(pageNumber, pageSize);

            var validation = await validator.ValidateAsync(request);

            if (!validation.IsValid)
            {
                return TypedResults.ValidationProblem(validation.ToDictionary());
            }

            var result = await handler.GetBooks(request);

            return TypedResults.Ok(result);
        });

        bookReaderGroup.MapGet("/pages", async Task<Results<Ok<int>, ValidationProblem>>
            ([FromQuery] int? pageSize, 
             [FromServices] IValidator<CountPagesRequest> validator, 
             [FromServices] ICountPaginationPages<Book> handler) =>
        {
            var request = new CountPagesRequest(pageSize);

            var validation = await validator.ValidateAsync(request);

            if (!validation.IsValid)
            {
                return TypedResults.ValidationProblem(validation.ToDictionary());
            }

            var result = await handler.CountByPageSize(request);

            return TypedResults.Ok(result);
        });

        bookReaderGroup.MapGet("/{bookId}", async Task<Results<Ok<FetchBookResponse>, ValidationProblem>>
            ([FromRoute] int bookId, 
             [FromServices] IValidator<FetchBookRequest> validator, 
             [FromServices] IBookReaderService handler) =>
        {
            var request = new FetchBookRequest { Id = bookId };

            var validation = await validator.ValidateAsync(request);

            if (!validation.IsValid)
            {
                return TypedResults.ValidationProblem(validation.ToDictionary());
            }

            var result = await handler.GetBook(request);

            return TypedResults.Ok(result);
        });
    }

    private static IDictionary<string, string[]> ToDictionary(this ValidationResult validationResult)
    {
        return validationResult.Errors
          .GroupBy(x => x.PropertyName)
          .ToDictionary(
            g => g.Key,
            g => g.Select(x => x.ErrorMessage).ToArray()
          );
    }
}
