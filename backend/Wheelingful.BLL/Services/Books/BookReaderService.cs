using Microsoft.EntityFrameworkCore;
using Wheelingful.BLL.Contracts.Auth;
using Wheelingful.BLL.Contracts.Books;
using Wheelingful.BLL.Extensions.Generic;
using Wheelingful.BLL.Models.Requests;
using Wheelingful.BLL.Models.Responses;
using Wheelingful.BLL.Models.Responses.Generic;
using Wheelingful.DAL.DbContexts;

namespace Wheelingful.BLL.Services.Books;

public class BookReaderService(
    ICurrentUser currentUser, 
    IBookCoverService bookCover,
    WheelingfulDbContext dbContext) : IBookReaderService
{
    public async Task<FetchPaginationResponse<FetchBookResponse>> GetBooks(FetchBookPaginationRequest request)
    {
        var query = dbContext.Books
            .Include(b => b.Users)
            .AsQueryable();

        if (request.DoFetchByCurrentUser != null)
        {
            if (request.DoFetchByCurrentUser.Value)
            {
                query = query.Where(b => b.Users.First().Id == currentUser.Id);
            }
        }

        var books = await query
            .Select(b => new FetchBookResponse
            {
                Id = b.Id,
                Title = b.Title,
                Description = b.Description,
                Category = b.Category.ToString(),
                Status = b.Status.ToString(),
                CoverUrl = bookCover.GetCoverUrl(b.Id, b.Users.First().Id)
            })
            .ToPagedListAsync(request.PageNumber.Value, request.PageSize.Value);

        var pageCount = await query.CountPages(request.PageSize.Value);

        return new FetchPaginationResponse<FetchBookResponse>
        {
            PageCount = pageCount,
            Items = books
        };
    }

    public Task<FetchBookResponse> GetBook(FetchBookRequest request)
    {
        return dbContext.Books
            .Include(b => b.Users)
            .Select(b => new FetchBookResponse
            {
                Id = b.Id,
                Title = b.Title,
                Description = b.Description,
                Category = b.Category.ToString(),
                Status = b.Status.ToString(),
                CoverUrl = bookCover.GetCoverUrl(b.Id, b.Users.First().Id)
            })
            .FirstAsync(b => b.Id == request.BookId);
    }
}
