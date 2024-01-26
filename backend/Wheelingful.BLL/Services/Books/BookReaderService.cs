using Microsoft.EntityFrameworkCore;
using Wheelingful.BLL.Contracts.Books;
using Wheelingful.BLL.Extensions.Generic;
using Wheelingful.BLL.Models.Requests;
using Wheelingful.BLL.Models.Requests.General;
using Wheelingful.BLL.Models.Responses;
using Wheelingful.DAL.DbContexts;

namespace Wheelingful.BLL.Services.Books;

public class BookReaderService(WheelingfulDbContext dbContext) : IBookReaderService
{
    public Task<List<FetchBookResponse>> GetBooks(FetchPaginationRequest request)
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
            .Paginate(request.PageNumber, request.PageSize)
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
            .FirstAsync(b => b.Id == request.Id);
    }
}
