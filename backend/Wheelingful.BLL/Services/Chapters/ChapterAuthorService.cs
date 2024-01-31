using Microsoft.EntityFrameworkCore;
using Wheelingful.BLL.Contracts.Chapters;
using Wheelingful.BLL.Models.Requests;
using Wheelingful.DAL.DbContexts;
using Wheelingful.DAL.Entities;

namespace Wheelingful.BLL.Services.Chapters;

public class ChapterAuthorService(
    IChapterTextService textService, 
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
    }

    public Task UpdateChapterProperties(UpdateChapterPropertiesRequest request)
    {
        return dbContext.Chapters
            .Where(c => c.Id == request.ChapterId)
            .ExecuteUpdateAsync(setters => setters
                .SetProperty(c => c.Title, request.Title)
                .SetProperty(c => c.UpdatedAt, DateTime.UtcNow));
    }

    public Task UpdateChapterText(UpdateChapterTextRequest request)
    {
        return textService.WriteText(request.Text, request.ChapterId, request.BookId);
    }


    public Task DeleteChapter(DeleteChapterRequest request)
    {
        textService.DeleteByChapter(request.ChapterId, request.BookId);

        return dbContext.Chapters
            .Where(c => c.Id == request.ChapterId)
            .ExecuteDeleteAsync();
    }
}
