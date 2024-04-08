using MediatR;
using Microsoft.Extensions.Logging;
using Wheelingful.BLL.Contracts.Auth;
using Wheelingful.BLL.Models.Requests.Commands;
using Wheelingful.BLL.Services.Books;
using Wheelingful.DAL.Contracts;
using Wheelingful.DAL.DbContexts;
using Wheelingful.DAL.Entities;
using Wheelingful.DAL.Helpers;

namespace Wheelingful.BLL.Handlers;

public class CreateReviewHandler(
    ICurrentUser currentUser,
    ILogger<ReviewService> logger,
    ICacheService cacheService,
    WheelingfulDbContext dbContext) : IRequestHandler<CreateReviewCommand>
{
    public async Task Handle(CreateReviewCommand request, CancellationToken cancellationToken)
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

        var entryKey = nameof(Review).ToCachePrefix(request.BookId) + currentUser.Id;

        await cacheService.RemoveByKey(entryKey);

        var listPrefix = nameof(Review).ToCachePrefix();

        await cacheService.RemoveByPrefix(listPrefix);
    }
}
