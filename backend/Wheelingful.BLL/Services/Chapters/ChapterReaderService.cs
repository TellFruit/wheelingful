using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Wheelingful.BLL.Constants;
using Wheelingful.BLL.Contracts.Auth;
using Wheelingful.BLL.Contracts.Chapters;
using Wheelingful.BLL.Extensions.Generic;
using Wheelingful.BLL.Models.Requests;
using Wheelingful.BLL.Models.Responses;
using Wheelingful.BLL.Models.Responses.Generic;
using Wheelingful.DAL.Contracts;
using Wheelingful.DAL.DbContexts;
using Wheelingful.DAL.Entities;
using Wheelingful.DAL.Helpers;

namespace Wheelingful.BLL.Services.Chapters;

public class ChapterReaderService(
    ICurrentUser currentUser,
    IChapterTextService textService,
    ILogger<ChapterReaderService> logger,
    ICacheService cacheService,
    WheelingfulDbContext dbContext) : IChapterReaderService
{
    public Task<FetchPaginationResponse<FetchChapterPropsResponse>> GetChapters(FetchChapterPaginationRequest request)
    {
        logger.LogInformation("User {UserId} fetched chapters: {@Request}",
            currentUser.Id, request);

        var query = dbContext.Chapters
            .Where(c => c.BookId == request.BookId)
            .OrderBy(c => c.CreatedAt)
            .Select(c => new FetchChapterPropsResponse
            {
                Id = c.Id,
                Title = c.Title,
                Date = c.CreatedAt.ToShortDateString(),
            });

        var prefix = nameof(Chapter).ToCachePrefix();

        var key = CacheHelper.GetCacheKey(prefix, new { request.BookId });

        var fetchValue = () => query.ToPagedListAsync(request.PageNumber.Value, request.PageSize.Value);

        if (FiltersAreStandard(request))
        {
            return cacheService.GetAndSet(key, fetchValue, CacheHelper.DefaultCacheExpiration);
        }

        return fetchValue();
    }

    public async Task<FetchChapterResponse> GetChapter(FetchChapterRequest request)
    {
        logger.LogInformation("User {UserId} fetched a chapter: {@Request}",
            currentUser.Id, request);

        var key = nameof(Chapter).ToCachePrefix(request.ChapterId);

        var chapter = await cacheService.GetAndSet(key, () =>
        {
            return dbContext.Chapters.FirstAsync(c => c.Id == request.ChapterId);
        },
        CacheHelper.DefaultCacheExpiration);

        return new FetchChapterResponse
        {
            Id = chapter.Id,
            Title = chapter.Title,
            Text = await textService.ReadText(chapter.Id, chapter.BookId),
        };
    }

    private bool FiltersAreStandard(FetchChapterPaginationRequest request)
    {
        return request.PageSize == PaginationConstants.DefaultPageSize
            && request.PageNumber == PaginationConstants.DefaultPageNumber;
    }
}
