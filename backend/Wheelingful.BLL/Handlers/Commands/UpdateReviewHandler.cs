using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Wheelingful.BLL.Contracts.Auth;
using Wheelingful.BLL.Models.Requests.Commands;
using Wheelingful.BLL.Services.Books;
using Wheelingful.DAL.Contracts;
using Wheelingful.DAL.DbContexts;
using Wheelingful.DAL.Entities;
using Wheelingful.DAL.Helpers;

namespace Wheelingful.BLL.Handlers.Commands;

public class UpdateReviewHandler(
    ICurrentUser currentUser,
    ILogger<ReviewService> logger,
    ICacheService cacheService,
    WheelingfulDbContext dbContext) : IRequestHandler<UpdateReviewCommand>
{
    public async Task Handle(UpdateReviewCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("User {UserId} updated a review: {@Request}",
            currentUser.Id, request);

        await dbContext.Reviews
            .Where(r => r.BookId == request.BookId && r.UserId == currentUser.Id)
            .ExecuteUpdateAsync(setters => setters
                .SetProperty(r => r.Title, request.Title)
                .SetProperty(r => r.Text, request.Text)
                .SetProperty(r => r.Score, request.Score)
                .SetProperty(r => r.UpdatedAt, DateTime.UtcNow));

        var entryKey = nameof(Review).ToCachePrefix(request.BookId) + currentUser.Id;

        await cacheService.RemoveByKey(entryKey);

        var listPrefix = nameof(Review).ToCachePrefix();

        await cacheService.RemoveByPrefix(listPrefix);
    }
}
