using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Wheelingful.BLL.Constants;
using Wheelingful.BLL.Contracts.Auth;
using Wheelingful.BLL.Contracts.Books;
using Wheelingful.BLL.Extensions.Generic;
using Wheelingful.BLL.Models.Predictions;
using Wheelingful.BLL.Models.Requests.Queries;
using Wheelingful.BLL.Models.Responses;
using Wheelingful.DAL.Contracts;
using Wheelingful.DAL.DbContexts;
using Wheelingful.DAL.Entities;
using Wheelingful.DAL.Helpers;

namespace Wheelingful.BLL.Handlers.Queries;

public class RecommendByBookHandler(
    ICurrentUser currentUser,
    ILogger<RecommendByUserHandler> logger,
    ICacheService cacheService,
    IBookCoverService bookCover,
    IBookRecommenderService bookRecommender,
    WheelingfulDbContext dbContext) : IRequestHandler<RecommendByBookQuery, IEnumerable<FetchBookResponse>>
{
    public async Task<IEnumerable<FetchBookResponse>> Handle(RecommendByBookQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("User {UserId} fetched a list of similar recommendations: {@Request}",
            currentUser.Id, request);

        var newGuidString = Guid.NewGuid().ToString();

        var newData = new List<ModelInput>
        {
            new ModelInput
            {
                BookId = request.BookId,
                UserId = newGuidString,
                ReviewScore = 5
            }
        };

        var prediction = await bookRecommender.BuildPredictionEngine(newData);

        var prefix = nameof(Book).ToCachePrefix();

        var query = dbContext.Books
            .Include(b => b.User)
            .Include(b => b.Reviews)
            .AsQueryable();

        var selected = query.Select(b => new
        {
            b.Id,
            b.Title,
            b.Description,
            b.Category,
            b.Status,
            b.CoverId,
            AuthorUserName = b.User.UserName!,
            AuthorUserId = b.UserId,
            b.Reviews
        });

        var fetchBooks = () => selected.ToListAsync();

        var books = await cacheService.GetAndSet(prefix, fetchBooks, CacheHelper.DefaultCacheExpiration);

        var reviews = await dbContext.Reviews
            .Where(r => r.UserId == currentUser.Id)
            .ToListAsync();

        var currentUserReviews = await dbContext.Reviews
            .Where(r => r.UserId == currentUser.Id)
            .ToListAsync();

        prefix = nameof(Book).ToCachePrefix(request.BookId);

        var key = CacheHelper.GetCacheKey(prefix, new { request, currentUser.Id });

        var fetchRecommended = () => books
            .Select(b =>
            {
                var modelInput = new ModelInput
                {
                    BookId = b.Id,
                    UserId = newGuidString,
                };

                var modelOutput = prediction.Predict(modelInput);

                return new { Book = b, PredictedScore = modelOutput.Score };
            })
            .Where(o => !currentUserReviews.Any(r => r.BookId == o.Book.Id)
                      && o.PredictedScore >= MlConstants.ScoreMinThreshold
                      && o.PredictedScore <= MlConstants.ScoreMaxThreshold)
            .OrderByDescending(o => o.PredictedScore)
            .Select(o => new FetchBookResponse
            {
                Id = o.Book.Id,
                Title = o.Book.Title,
                Description = o.Book.Description,
                Category = o.Book.Category,
                Status = o.Book.Status,
                CoverUrl = bookCover.GetCoverUrl(o.Book.Id, o.Book.AuthorUserId, o.Book.CoverId),
                AuthorUserName = o.Book.AuthorUserName,
                AverageScore = (int)o.Book.Reviews.Average(r => r.Score)
            })
            .Take(PaginationConstants.DefaultPageSize);

        return await cacheService.GetAndSet(key, fetchRecommended, CacheHelper.DefaultCacheExpiration);
    }
}
