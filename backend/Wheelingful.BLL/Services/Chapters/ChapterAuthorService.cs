using Microsoft.EntityFrameworkCore;
using Wheelingful.BLL.Contracts.Chapters;
using Wheelingful.BLL.Models.Requests;
using Wheelingful.DAL.DbContexts;
using Wheelingful.DAL.Entities;

namespace Wheelingful.BLL.Services.Chapters;

public class ChapterAuthorService(WheelingfulDbContext dbContext) : IChapterAuthorService
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
    }

    public Task UpdateChapter(UpdateChapterRequest request)
    {
        return dbContext.Chapters
            .Where(c => c.Id == request.Id)
            .ExecuteUpdateAsync(setters => setters
                .SetProperty(c => c.Title, request.Title)
                .SetProperty(c => c.UpdatedAt, DateTime.UtcNow));
    }

    public Task DeleteChapter(DeleteChapterRequest request)
    {
        return dbContext.Chapters
            .Where(c => c.Id == request.Id)
            .ExecuteDeleteAsync();
    }
}
