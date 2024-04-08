using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Wheelingful.BLL.Constants;
using Wheelingful.BLL.Contracts.Auth;
using Wheelingful.BLL.Contracts.Books;
using Wheelingful.BLL.Extensions.Generic;
using Wheelingful.BLL.Models.Requests;
using Wheelingful.BLL.Models.Responses;
using Wheelingful.BLL.Models.Responses.Generic;
using Wheelingful.DAL.Contracts;
using Wheelingful.DAL.DbContexts;
using Wheelingful.DAL.Entities;
using Wheelingful.DAL.Helpers;

namespace Wheelingful.BLL.Services.Books;

public class ReviewService(
    ICurrentUser currentUser,
    ILogger<ReviewService> logger,
    ICacheService cacheService,
    WheelingfulDbContext dbContext) : IReviewService
{
    public Task<FetchPaginationResponse<FetchReviewResponse>> GetReviews(FetchReviewPaginationRequest request)
    {
        logger.LogInformation("User {UserId} fetched review list with parameters: {@Request}",
            currentUser.Id, request);

        var query = dbContext.Reviews
            .AsQueryable();

        var doFetchByCurrentUser = request.DoFetchByCurrentUser.HasValue
            && request.DoFetchByCurrentUser.Value;

        if (doFetchByCurrentUser)
        {
            query = query.Where(r => r.UserId == currentUser.Id);
        }

        if (request.BookId.HasValue)
        {
            query = query.Where(r => r.BookId == request.BookId.Value);
        }

        var selected = query.Select(r => new FetchReviewResponse
        {
            BookId = r.BookId,
            UserId = r.UserId,
            UserName = r.User.UserName!,
            Text = r.Text,
            Title = r.Title,
            Score = r.Score,
            CreatedAt = r.CreatedAt.ToString("M/d/yyyy, h:mm tt"),
        });

        selected = selected.OrderByDescending(r => r.UserId == currentUser.Id);

        var fetchValue = () => selected.ToPagedListAsync(request.PageNumber.Value, request.PageSize.Value);

        if (FiltersAreStandard(request))
        {
            var prefix = nameof(Review).ToCachePrefix();

            var key = CacheHelper.GetCacheKey(prefix, new { request.BookId });

            if (doFetchByCurrentUser)
            {
                key = CacheHelper.GetCacheKey(prefix, new { request.BookId, currentUser.Id });
            }

            return cacheService.GetAndSet(key, fetchValue, CacheHelper.DefaultCacheExpiration);
        }

        return fetchValue();
    }

    public Task<FetchReviewResponse> GetReview(FetchReviewRequest request)
    {
        logger.LogInformation("User {UserId} fetched the review: {@Request}",
            currentUser.Id, request);

        var key = nameof(Review).ToCachePrefix(request.BookId) + currentUser.Id;

        return cacheService.GetAndSet(key, () =>
        {
            return dbContext.Reviews
            .Include(r => r.User)
            .Select(r => new FetchReviewResponse
            {
                BookId = r.BookId,
                UserId = r.UserId,
                UserName = r.User.UserName!,
                Text = r.Text,
                Title = r.Title,
                Score = r.Score,
                CreatedAt = r.CreatedAt.ToString("M/d/yyyy, h:mm tt"),
            })
            .FirstAsync(r => r.BookId == request.BookId && r.UserId == currentUser.Id);
        },
        CacheHelper.DefaultCacheExpiration);
    }

    private bool FiltersAreStandard(FetchReviewPaginationRequest request)
    {
        return request.PageSize == PaginationConstants.DefaultPageSize
            && request.PageNumber == PaginationConstants.DefaultPageNumber;
    }
}
