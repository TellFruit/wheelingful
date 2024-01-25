using Microsoft.EntityFrameworkCore;
using Wheelingful.BLL.Contracts.Books;
using Wheelingful.BLL.Extensions.Generic;
using Wheelingful.BLL.Models.Books;
using Wheelingful.BLL.Models.General;
using Wheelingful.DAL.DbContexts;

namespace Wheelingful.BLL.Services.Books;

public class BookReaderService(WheelingfulDbContext dbContext) : IBookReaderService
{
    public Task<List<FetchBookModel>> GetBooks(FetchRequest request)
    {
        return dbContext
            .Books
            .Include(b => b.Users)
            .Select(b => new FetchBookModel
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
}
