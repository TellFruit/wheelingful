using Microsoft.EntityFrameworkCore;
using Wheelingful.Core.Contracts.Books;
using Wheelingful.Core.DTO.Books;
using Wheelingful.Data.DbContexts;
using Wheelingful.Data.Entities;

namespace Wheelingful.Data.Outer;

internal class BookManager : IBookManager
{
    private readonly WheelingfulDbContext _dbContext;

    public BookManager(WheelingfulDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task CreateBook(NewBookModel model)
    {
        var newBook = new Book
        {
            Title = model.Title,
            Description = model.Description,
            Category = model.Category,
            Status = model.Status,
            CoverId = model.CoverId,
        };

        var author = _dbContext.Users
            .First(u => u.Id == model.AuthorId);

        newBook.Users.Add(author);

        _dbContext.Add(newBook);

        await _dbContext.SaveChangesAsync();
    }

    public IQueryable<ReadBookModel> GetBooks()
    {
        return _dbContext.Books
            .Include(b => b.Users)
            .Select(b => new ReadBookModel
            {
                Id = b.Id,
                Title = b.Title,
                Description = b.Description,
                Category = b.Category,
                Status = b.Status,
                AuthorId = b.Users.First().Id,
                CoverId = b.CoverId,
            });
    }

    public async Task UpdateBook(UpdatedBookModel model)
    {
        var book = _dbContext.Books.First(b => b.Id == model.Id);

        book.Title = model.Title;
        book.Description = model.Description;
        book.Category = model.Category;
        book.Status = model.Status;
        book.CoverId = model.CoverId ?? book.CoverId;

        _dbContext.Update(book);

        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteBook(int bookId)
    {
        var book = _dbContext.Books.First(b => b.Id == bookId);

        _dbContext.Remove(book);

        await _dbContext.SaveChangesAsync();
    }
}
