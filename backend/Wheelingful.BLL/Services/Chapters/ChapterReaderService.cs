using Microsoft.EntityFrameworkCore;
using Wheelingful.BLL.Contracts.Chapters;
using Wheelingful.BLL.Extensions.Generic;
using Wheelingful.BLL.Models.Requests;
using Wheelingful.BLL.Models.Responses;
using Wheelingful.BLL.Models.Responses.Generic;
using Wheelingful.DAL.DbContexts;

namespace Wheelingful.BLL.Services.Chapters;

public class ChapterReaderService(
    IChapterTextService textService,
    WheelingfulDbContext dbContext) : IChapterReaderService
{
    public Task<FetchPaginationResponse<FetchChapterPropsResponse>> GetChapters(FetchChapterPaginationRequest request)
    {
        var query = dbContext.Chapters
            .Where(c => c.BookId == request.BookId)
            .OrderBy(c => c.CreatedAt)
            .AsQueryable();

        return query
            .Select(c => new FetchChapterPropsResponse
            {
                Id = c.Id,
                Title = c.Title,
                Date = c.CreatedAt.ToShortDateString(),
            })
            .ToPagedListAsync(request.PageNumber.Value, request.PageSize.Value);
    }

    public async Task<FetchChapterResponse> GetChapter(FetchChapterRequest request)
    {
        var chapter = await dbContext.Chapters.FirstAsync(c => c.Id == request.ChapterId);

        var text = await textService.ReadText(chapter.Id, chapter.BookId);

        return new FetchChapterResponse
        {
            Id = chapter.Id,
            Title = chapter.Title, 
            Text = text 
        };
    }
}
