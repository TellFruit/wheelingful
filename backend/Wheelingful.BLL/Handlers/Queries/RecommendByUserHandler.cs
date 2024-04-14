using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Wheelingful.BLL.Constants;
using Wheelingful.BLL.Contracts.Auth;
using Wheelingful.BLL.Contracts.Books;
using Wheelingful.BLL.Models.Predictions;
using Wheelingful.BLL.Models.Requests.Queries;
using Wheelingful.BLL.Models.Responses;
using Wheelingful.DAL.Contracts;
using Wheelingful.DAL.DbContexts;
using Wheelingful.DAL.Entities;
using Wheelingful.DAL.Helpers;

namespace Wheelingful.BLL.Handlers.Queries;

public class RecommendByUserHandler(
    ICurrentUser currentUser,
    ILogger<RecommendByUserHandler> logger,
    ICacheService cacheService,
    IBookCoverService bookCover,
    IBookRecommenderService bookRecommender,
    WheelingfulDbContext dbContext) : IRequestHandler<RecommendByUserQuery, IEnumerable<FetchBookResponse>>
{
    public async Task<IEnumerable<FetchBookResponse>> Handle(RecommendByUserQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("User {UserId} fetched a personal recommendations list: {@Request}",
            currentUser.Id, request);

        var prediction = await bookRecommender.BuildPredictionEngine();

        var prefix = nameof(Book).ToCachePrefix();

        var query = dbContext.Books
            .Include(b => b.User)
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
        });

        var fetchValue = () => selected.ToListAsync();

        var books = await cacheService.GetAndSet(prefix, fetchValue, CacheHelper.DefaultCacheExpiration);

        var currentUserReviews = await dbContext.Reviews
            .Where(r => r.UserId == currentUser.Id)
            .ToListAsync();

        return books
            .Select(b =>
            {
                var modelInput = new ModelInput
                {
                    BookId = b.Id,
                    UserId = currentUser.Id
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
            })
            .Take(PaginationConstants.DefaultPageSize);
    }
}
