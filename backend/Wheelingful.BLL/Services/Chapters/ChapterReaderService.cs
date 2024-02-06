using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Wheelingful.BLL.Contracts.Chapters;
using Wheelingful.BLL.Contracts.Generic;
using Wheelingful.BLL.Extensions.Generic;
using Wheelingful.BLL.Models.Requests;
using Wheelingful.BLL.Models.Responses;
using Wheelingful.DAL.DbContexts;
using Wheelingful.DAL.Entities;

namespace Wheelingful.BLL.Services.Chapters;

public class ChapterReaderService(
    IChapterTextService textService,
    ICountPaginationPages<Chapter> countPaginationPages,
    WheelingfulDbContext dbContext) : IChapterReaderService
{
    public Task<List<FetchChapterPaginatedResponse>> GetChapters(FetchChapterPaginationRequest request)
    {
        return dbContext
            .Chapters
            .Where(c => c.BookId == request.BookId)
            .Select(c => new FetchChapterPaginatedResponse
            { 
                Id = c.Id,
                Title = c.Title
            })
            .Paginate(request.PageNumber.Value, request.PageSize.Value)
            .ToListAsync();
    }

    public Task<int> CountPaginationPages(CountChapterPaginationPagesRequest request)
    {
        var filters = new List<Expression<Func<Chapter, bool>>>
        {
            c => c.BookId == request.BookId
        };

        return countPaginationPages.CountByPageSize(request, filters);
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
