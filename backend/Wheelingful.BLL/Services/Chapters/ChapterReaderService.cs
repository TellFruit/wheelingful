using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Text.Json;
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
        logger.LogInformation("User {userId} fetched chapters: {request}",
            currentUser.Id, JsonSerializer.Serialize(request));

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

        var key = CacheHelper.GetCacheKey(prefix, request);

        return cacheService.GetAndSet(key, () =>
        {
            return query.ToPagedListAsync(request.PageNumber.Value, request.PageSize.Value);
        },
        CacheHelper.DefaultCacheExpiration);
    }

    public async Task<FetchChapterResponse> GetChapter(FetchChapterRequest request)
    {
        logger.LogInformation("User {userId} fetched a chapter: {request}",
            currentUser.Id, JsonSerializer.Serialize(request));

        var prefix = nameof(Chapter).ToCachePrefix(request.ChapterId);

        var key = CacheHelper.GetCacheKey(prefix, request);

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
}
