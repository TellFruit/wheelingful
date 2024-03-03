using Microsoft.EntityFrameworkCore;
using Wheelingful.BLL.Contracts.Chapters;
using Wheelingful.BLL.Models.Requests;
using Wheelingful.DAL.Contracts;
using Wheelingful.DAL.DbContexts;
using Wheelingful.DAL.Entities;
using Wheelingful.DAL.Helpers;

namespace Wheelingful.BLL.Services.Chapters;

public class ChapterAuthorService(
    IChapterTextService textService,
    ICacheService cacheService,
    WheelingfulDbContext dbContext) : IChapterAuthorService
{
    public async Task CreateChapter(CreateChapterRequest request)
    {
        var newChapter = new Chapter
        {
            Title = request.Title,
            BookId = request.BookId,
        };

        dbContext.Add(newChapter);

        await dbContext.SaveChangesAsync();

        await textService.WriteText(request.Text, newChapter.Id, request.BookId);

        var prefix = nameof(Chapter).ToCachePrefix();

        await cacheService.RemoveByPrefix(prefix);
    }

    public async Task UpdateChapter(UpdateChapterRequest request)
    {
        await dbContext.Chapters
            .Where(c => c.Id == request.ChapterId)
            .ExecuteUpdateAsync(setters => setters
                .SetProperty(c => c.Title, request.Title)
                .SetProperty(c => c.UpdatedAt, DateTime.UtcNow));

        if (request.Text != null)
        {
            await textService.WriteText(request.Text, request.ChapterId, request.BookId);
        }

        var prefix = nameof(Chapter).ToCachePrefix(request.ChapterId);

        await cacheService.RemoveByPrefix(prefix);
    }

    public async Task DeleteChapter(DeleteChapterRequest request)
    {
        textService.DeleteByChapter(request.ChapterId, request.BookId);

        await dbContext.Chapters
            .Where(c => c.Id == request.ChapterId)
            .ExecuteDeleteAsync();

        var prefix = nameof(Chapter).ToCachePrefix(request.ChapterId);

        await cacheService.RemoveByPrefix(prefix);
    }
}
