using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Wheelingful.API.Constants;
using Wheelingful.API.Models.Bindings;
using Wheelingful.BLL.Contracts.Books;
using Wheelingful.BLL.Models.Requests;
using Wheelingful.BLL.Models.Responses.Generic;
using Wheelingful.BLL.Models.Responses;
using Wheelingful.API.Models;
using MediatR;
using Wheelingful.BLL.Models.Requests.Commands;
using Wheelingful.BLL.Models.Requests.Queries;

namespace Wheelingful.API.Extensions.Endpoints;

public static class UserExtension
{
    public static void MapUserApi(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost("/current/books/{bookId}/reviews", async Task<Results<Created, ValidationProblem>> (
             [AsParameters] CreateReviewBinding request, [FromServices] IMediator mediator) =>
        {
            await mediator.Send(request.To());

            return TypedResults.Created();
        })
            .RequireAuthorization(PolicyContants.AuthorizeReader);

        endpoints.MapPut("/current/books/{bookId}/reviews", async Task<Results<Ok, ValidationProblem>>
            ([AsParameters] UpdateReviewBinding request, [FromServices] IMediator mediator) =>
        {
            await mediator.Send(request.To());

            return TypedResults.Ok();
        })
            .RequireAuthorization(PolicyContants.AuthorizeReader);

        endpoints.MapDelete("/current/books/{bookId}/reviews", async Task<Results<NoContent, ValidationProblem>>
            ([AsParameters] DeleteReviewCommand request, [FromServices] IMediator mediator) =>
        {
            await mediator.Send(request);

            return TypedResults.NoContent();
        })
            .RequireAuthorization(PolicyContants.AuthorizeReader);

        endpoints.MapGet("/current/reviews",
            async Task<Results<Ok<FetchPaginationResponse<FetchReviewResponse>>, ValidationProblem>>
                ([AsParameters] FetchReviewsByCurrentUserRequest request, [FromServices] IReviewService handler) =>
            {
                var result = await handler.GetReviews(request);

                return TypedResults.Ok(result);
            })
                .RequireAuthorization(PolicyContants.AuthorizeReader);

        endpoints.MapGet("/current/books/{bookId}/reviews",
            async Task<Results<Ok<FetchReviewResponse>, ValidationProblem>>
                ([AsParameters] FetchReviewRequest request, [FromServices] IReviewService handler) =>
            {
                var result = await handler.GetReview(request);

                return TypedResults.Ok(result);
            })
                .RequireAuthorization(PolicyContants.AuthorizeReader);

        endpoints.MapGet("/current/recommendations",
            async Task<Results<Ok<IEnumerable<FetchBookResponse>>, ValidationProblem>>
                ([AsParameters] RecommendByUserQuery request, [FromServices] IMediator mediator) =>
            {
                var resut = await mediator.Send(request);

                return TypedResults.Ok(resut);
            })
                .RequireAuthorization(PolicyContants.AuthorizeReader);
    }
}
