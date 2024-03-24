using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Wheelingful.API.Constants;
using Wheelingful.API.Models.Bindings;
using Wheelingful.BLL.Contracts.Books;
using Wheelingful.BLL.Models.Requests;
using Wheelingful.BLL.Models.Responses.Generic;
using Wheelingful.BLL.Models.Responses;
using Wheelingful.API.Models;

namespace Wheelingful.API.Extensions.Endpoints;

public static class UserExtension
{
    public static void MapUserApi(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost("/current/books/{bookId}/reviews", async Task<Results<Created, ValidationProblem>> (
             [FromBody] CreateReviewBinding request, [FromServices] IReviewService handler) =>
        {
            await handler.CreateReview(request.To());

            return TypedResults.Created();
        })
            .RequireAuthorization(PolicyContants.AuthorizeReader);

        endpoints.MapPut("/current/books/{bookId}/reviews", async Task<Results<Ok, ValidationProblem>>
            ([AsParameters] UpdateReviewBinding request, [FromServices] IReviewService handler) =>
        {
            await handler.UpdateReview(request.To());

            return TypedResults.Ok();
        })
            .RequireAuthorization(PolicyContants.AuthorizeAuthor);

        endpoints.MapDelete("/current/books/{bookId}/reviews", async Task<Results<NoContent, ValidationProblem>>
            ([AsParameters] DeleteReviewRequest request, [FromServices] IReviewService handler) =>
        {
            await handler.DeleteReview(request);

            return TypedResults.NoContent();
        })
            .RequireAuthorization(PolicyContants.AuthorizeAuthor);

        endpoints.MapGet("/current/reviews", 
            async Task<Results<Ok<FetchPaginationResponse<FetchReviewResponse>>, ValidationProblem>>
                ([AsParameters] FetchReviewsByCurrentUserRequest request, [FromServices] IReviewService handler) =>
            {
                var result = await handler.GetReviews(request);

                return TypedResults.Ok(result);
            });

        endpoints.MapGet("/current/books/{bookId}/reviews",
            async Task<Results<Ok<FetchReviewResponse>, ValidationProblem>>
                ([AsParameters] FetchReviewRequest request, [FromServices] IReviewService handler) =>
            {
                var result = await handler.GetReview(request);

                return TypedResults.Ok(result);
            });
    }
}
