using Microsoft.EntityFrameworkCore;
using Wheelingful.BLL.Contracts.Books;
using Wheelingful.BLL.Extensions.Generic;
using Wheelingful.BLL.Models.Requests;
using Wheelingful.BLL.Models.Responses;
using Wheelingful.DAL.DbContexts;

namespace Wheelingful.BLL.Services.Books;

public class BookReaderService(WheelingfulDbContext dbContext) : IBookReaderService
{
    public Task<List<FetchBookResponse>> GetBooks(FetchBookPaginationRequest request)
    {
        var query = dbContext
            .Books
            .Include(b => b.Users)
            .Select(b => new FetchBookResponse
            {
                Id = b.Id,
                AuthorId = b.Users.First().Id,
                Title = b.Title,
                Description = b.Description,
                Category = b.Category,
                CoverId = b.CoverId,
            });

        if (request.AuthorId != null)
        {
            query = query.Where(c => c.AuthorId == request.AuthorId);
        }

        return query
            .Paginate(request.PageNumber.Value, request.PageSize.Value)
            .ToListAsync();
    }

    public Task<FetchBookResponse> GetBook(FetchBookRequest request)
    {
        return dbContext
            .Books
            .Include(b => b.Users)
            .Select(b => new FetchBookResponse
            {
                Id = b.Id,
                AuthorId = b.Users.First().Id,
                Title = b.Title,
                Description = b.Description,
                Category = b.Category,
                CoverId = b.CoverId,
            })
            .FirstAsync(b => b.Id == request.BookId);
    }
}
