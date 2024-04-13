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

public class DeleteReviewHandler(
    ICurrentUser currentUser,
    ILogger<ReviewService> logger,
    ICacheService cacheService,
    WheelingfulDbContext dbContext) : IRequestHandler<DeleteReviewCommand>
{
    public async Task Handle(DeleteReviewCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("User {UserId} deleted a review: {@Request}",
            currentUser.Id, request);

        await dbContext.Reviews
            .Where(r => r.BookId == request.BookId && r.UserId == currentUser.Id)
            .ExecuteDeleteAsync();

        var entryKey = nameof(Review).ToCachePrefix(request.BookId) + currentUser.Id;

        await cacheService.RemoveByKey(entryKey);

        var listPrefix = nameof(Review).ToCachePrefix();

        await cacheService.RemoveByPrefix(listPrefix);
    }
}
