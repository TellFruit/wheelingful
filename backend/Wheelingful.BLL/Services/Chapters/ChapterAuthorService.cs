using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Wheelingful.BLL.Contracts.Auth;
using Wheelingful.BLL.Contracts.Chapters;
using Wheelingful.BLL.Models.Requests;
using Wheelingful.DAL.Contracts;
using Wheelingful.DAL.DbContexts;
using Wheelingful.DAL.Entities;
using Wheelingful.DAL.Helpers;

namespace Wheelingful.BLL.Services.Chapters;

public class ChapterAuthorService(
    ICurrentUser currentUser,
    IChapterTextService textService,
    ICacheService cacheService,
    ILogger<ChapterAuthorService> logger,
    WheelingfulDbContext dbContext) : IChapterAuthorService
{
    public async Task CreateChapter(CreateChapterRequest request)
    {
        logger.LogInformation("User {UserId} created a chapter: {@Request}",
            currentUser.Id, request);

        var newChapter = new Chapter
        {
            Title = request.Title,
            BookId = request.BookId,
        };

        dbContext.Add(newChapter);

        await dbContext.SaveChangesAsync();

        await textService.WriteText(request.Text, newChapter.Id, request.BookId);

        var listPrefix = nameof(Chapter).ToCachePrefix();

        var key = CacheHelper.GetCacheKey(listPrefix, new { request.BookId });

        await cacheService.RemoveByKey(key);
    }

    public async Task UpdateChapter(UpdateChapterRequest request)
    {
        logger.LogInformation("User {UserId} updated the chapter: {@Request}",
            currentUser.Id, request);

        await dbContext.Chapters
            .Where(c => c.Id == request.ChapterId)
            .ExecuteUpdateAsync(setters => setters
                .SetProperty(c => c.Title, request.Title)
                .SetProperty(c => c.UpdatedAt, DateTime.UtcNow));

        if (request.Text != null)
        {
            await textService.WriteText(request.Text, request.ChapterId, request.BookId);
        }

        var entryById = nameof(Chapter).ToCachePrefix(request.ChapterId);

        await cacheService.RemoveByKey(entryById);

        var listPrefix = nameof(Chapter).ToCachePrefix();

        var listKey = CacheHelper.GetCacheKey(listPrefix, new { request.BookId });

        await cacheService.RemoveByKey(listKey);
    }

    public async Task DeleteChapter(DeleteChapterRequest request)
    {
        logger.LogInformation("User {UserId} deleted the chapter: {@Request}",
            currentUser.Id, request);

        textService.DeleteByChapter(request.ChapterId, request.BookId);

        await dbContext.Chapters
            .Where(c => c.Id == request.ChapterId)
            .ExecuteDeleteAsync();

        var listPrefix = nameof(Chapter).ToCachePrefix();

        var listKey = CacheHelper.GetCacheKey(listPrefix, new { request.BookId });

        await cacheService.RemoveByKey(listKey);
    }
}
