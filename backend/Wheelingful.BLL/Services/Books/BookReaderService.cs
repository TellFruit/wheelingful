using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using Wheelingful.BLL.Constants;
using Wheelingful.BLL.Contracts.Auth;
using Wheelingful.BLL.Contracts.Books;
using Wheelingful.BLL.Extensions.Generic;
using Wheelingful.BLL.Models.Requests;
using Wheelingful.BLL.Models.Responses;
using Wheelingful.BLL.Models.Responses.Generic;
using Wheelingful.DAL.Contracts;
using Wheelingful.DAL.DbContexts;
using Wheelingful.DAL.Entities;
using Wheelingful.DAL.Helpers;

namespace Wheelingful.BLL.Services.Books;

public class BookReaderService(
    ICurrentUser currentUser,
    IBookCoverService bookCover,
    ILogger<BookReaderService> logger,
    ICacheService cacheService,
    WheelingfulDbContext dbContext) : IBookReaderService
{
    public Task<FetchPaginationResponse<FetchBookResponse>> GetBooks(FetchBookPaginationRequest request)
    {
        logger.LogInformation("User {userId} fetched book list with parameters: {@request}",
            currentUser.Id, request);

        var query = dbContext.Books
            .Include(b => b.Users)
            .AsQueryable();

        var doFetchByCurrentUser = request.DoFetchByCurrentUser.HasValue
            && request.DoFetchByCurrentUser.Value;

        if (doFetchByCurrentUser)
        {
            query = query.Where(b => b.Users.First().Id == currentUser.Id);
        }

        var selected = query.Select(b => new FetchBookResponse
        {
            Id = b.Id,
            Title = b.Title,
            Description = b.Description,
            Category = b.Category,
            Status = b.Status,
            CoverUrl = bookCover.GetCoverUrl(b.Id, b.Users.First().Id)
        });

        var fetchValue = () => selected.ToPagedListAsync(request.PageNumber.Value, request.PageSize.Value);

        if (FiltersAreStandard(request))
        {
            var prefix = nameof(Book).ToCachePrefix();

            var key = CacheHelper.GetCacheKey(prefix, request);

            if (doFetchByCurrentUser)
            {
                key = CacheHelper.GetCacheKey(prefix, new { request, currentUser.Id });
            }

            return cacheService.GetAndSet(key, fetchValue, CacheHelper.DefaultCacheExpiration);
        }

        return fetchValue();
    }

    public Task<FetchBookResponse> GetBook(FetchBookRequest request)
    {
        logger.LogInformation("User {userId} fetched the book: {@request}",
            currentUser.Id, request);

        var key = nameof(Book).ToCachePrefix(request.BookId);

        return cacheService.GetAndSet(key, () =>
        {
            return dbContext.Books
            .Include(b => b.Users)
            .Select(b => new FetchBookResponse
            {
                Id = b.Id,
                Title = b.Title,
                Description = b.Description,
                Category = b.Category,
                Status = b.Status,
                CoverUrl = bookCover.GetCoverUrl(b.Id, b.Users.First().Id)
            })
            .FirstAsync(b => b.Id == request.BookId);
        },
        CacheHelper.DefaultCacheExpiration);
    }

    private bool FiltersAreStandard(FetchBookPaginationRequest request)
    {
        return request.PageSize == PaginationConstants.DefaultPageSize
            && request.PageNumber == PaginationConstants.DefaultPageNumber;
    }
}
