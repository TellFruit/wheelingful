using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Wheelingful.BLL.Contracts.Auth;
using Wheelingful.BLL.Contracts.Books;
using Wheelingful.BLL.Contracts.Generic;
using Wheelingful.BLL.Extensions.Generic;
using Wheelingful.BLL.Models.Requests;
using Wheelingful.BLL.Models.Responses;
using Wheelingful.DAL.DbContexts;
using Wheelingful.DAL.Entities;

namespace Wheelingful.BLL.Services.Books;

public class BookReaderService(
    ICurrentUser currentUser, 
    IBookCoverService bookCover,
    ICountPaginationPages<Book> countPaginationPages,
    WheelingfulDbContext dbContext) : IBookReaderService
{
    public Task<List<FetchBookResponse>> GetBooks(FetchBookPaginationRequest request)
    {
        var query = dbContext
            .Books
            .Include(b => b.Users)
            .AsQueryable();

        if (request.DoFetchByCurrentUser != null)
        {
            if (request.DoFetchByCurrentUser.Value)
            {
                query = query.Where(b => b.Users.First().Id == currentUser.Id);
            }
        }

        return query
            .Select(b => new FetchBookResponse
            {
                Id = b.Id,
                Title = b.Title,
                Description = b.Description,
                Category = b.Category.ToString(),
                Status = b.Status.ToString(),
                CoverUrl = bookCover.GetCoverUrl(b.Id, b.Users.First().Id)
            })
            .Paginate(request.PageNumber.Value, request.PageSize.Value)
            .ToListAsync();
    }

    public Task<int> CountPaginationPages(CountBookPaginationPagesRequest request)
    {
        var filters = new List<Expression<Func<Book, bool>>>();

        if (request.DoFetchByCurrentUser != null)
        {
            if (request.DoFetchByCurrentUser.Value)
            {
                filters.Add(b => b.Users.First().Id == currentUser.Id);
            }
        }

        return countPaginationPages.CountByPageSize(request, filters);
    }

    public Task<FetchBookResponse> GetBook(FetchBookRequest request)
    {
        return dbContext
            .Books
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
