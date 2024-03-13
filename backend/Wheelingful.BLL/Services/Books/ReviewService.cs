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
            .Where(r => r.BookId == request.BookId);

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
            Text = r.Text,
            Title = r.Title,
            Score = r.Score,
        });

        var fetchValue = () => selected.ToPagedListAsync(request.PageNumber.Value, request.PageSize.Value);

        if (FiltersAreStandard(request))
        {
            var prefix = nameof(Book).ToCachePrefix();

            var key = CacheHelper.GetCacheKey(prefix, request);

            if (doFetchByCurrentUser)
            {
                key = CacheHelper.GetCacheKey(prefix, new { request, currentUser.Id });
            }

            return cacheService.GetAndSet(key, fetchValue, CacheHelper.DefaultCacheExpiration);
        }

        return fetchValue();
    }

    public Task<FetchReviewResponse> GetReview(FetchReviewRequest request)
    {
        logger.LogInformation("User {UserId} fetched the review: {@Request}",
            currentUser.Id, request);

        var key = nameof(Review).ToCachePrefix(request.BookId) + request.UserId;

        return cacheService.GetAndSet(key, () =>
        {
            return dbContext.Reviews
            .Select(r => new FetchReviewResponse
            {
                BookId = r.BookId,
                UserId = r.UserId,
                Text = r.Text,
                Title = r.Title,
                Score = r.Score,
            })
            .FirstAsync(r => r.BookId == request.BookId && r.UserId == request.UserId);
        },
        CacheHelper.DefaultCacheExpiration);
    }

    public async Task CreateReview(CreateReviewRequest request)
    {
        logger.LogInformation("User {UserId} created a review: {@Request}",
            currentUser.Id, request);

        var newReview = new Review
        {
            BookId = request.BookId,
            UserId = currentUser.Id,
            Title = request.Title,
            Text = request.Text,
            Score = request.Score,
        };
        
        dbContext.Add(newReview);

        await dbContext.SaveChangesAsync();

        var listPrefix = nameof(Review).ToCachePrefix();

        var key = CacheHelper.GetCacheKey(listPrefix, new { request.BookId });

        await cacheService.RemoveByKey(key);
    }

    public async Task UpdateReview(UpdateReviewRequest request)
    {
        logger.LogInformation("User {UserId} updated a review: {@Request}",
            currentUser.Id, request);

        await dbContext.Reviews
            .Where(r => r.BookId == request.BookId && r.UserId == request.UserId)
            .ExecuteUpdateAsync(setters => setters
                .SetProperty(r => r.Title, request.Title)
                .SetProperty(r => r.Text, request.Text)
                .SetProperty(r => r.Score, request.Score)
                .SetProperty(r => r.UpdatedAt, DateTime.UtcNow));

        var listPrefix = nameof(Review).ToCachePrefix();

        var key = CacheHelper.GetCacheKey(listPrefix, new { request.BookId });

        await cacheService.RemoveByKey(key);
    }

    public async Task DeleteReview(DeleteReviewRequest request)
    {
        logger.LogInformation("User {UserId} deleted a review: {@Request}",
            currentUser.Id, request);

        await dbContext.Reviews
            .Where(r => r.BookId == request.BookId && r.UserId == request.UserId)
            .ExecuteDeleteAsync();

        var listPrefix = nameof(Review).ToCachePrefix();

        var key = CacheHelper.GetCacheKey(listPrefix, new { request.BookId });

        await cacheService.RemoveByKey(key);
    }

    private bool FiltersAreStandard(FetchReviewPaginationRequest request)
    {
        return request.PageSize == PaginationConstants.DefaultPageSize
            && request.PageNumber == PaginationConstants.DefaultPageNumber;
    }
}
