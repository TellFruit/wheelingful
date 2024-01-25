using Microsoft.EntityFrameworkCore;
using Wheelingful.Core.Contracts.Auth;
using Wheelingful.Core.Contracts.Books;
using Wheelingful.Core.DTO.Books;
using Wheelingful.Core.Extensions;
using Wheelingful.Data.DbContexts;
using Wheelingful.Data.Entities;

namespace Wheelingful.Core.Services.Books;

internal class BookAuthorService : IBookAuthorService
{
    private readonly IBookCoverManager _bookCover;
    private readonly ICurrentUser _currentUser;
    private readonly WheelingfulDbContext _dbContext;

    public BookAuthorService(IBookCoverManager bookCover, ICurrentUser currentUser, WheelingfulDbContext dbContext)
    {
        _bookCover = bookCover;
        _currentUser = currentUser;
        _dbContext = dbContext;
    }

    public async Task CreateBook(NewBookModel model)
    {
        model.AuthorId = _currentUser.Id;

        model.CoverId = await _bookCover.UploadCover(model.CoverBase64, model.Title, model.AuthorId);

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

    public async Task UpdateBook(UpdatedBookModel model)
    {
        var isActualAuthor = await _dbContext.Books.IsActualAuthor(model.Id, _currentUser.Id);

        if (!isActualAuthor)
        {
            return;
        }

        if (model.CoverBase64 != null)
        {
            var oldCoverId = await _dbContext
                .Books
                .Where(b => b.Id == model.Id)
                .Select(b => b.CoverId)
                .FirstAsync();

            model.CoverId = await _bookCover.UpdateCover(oldCoverId, model.CoverBase64, model.Title, _currentUser.Id);
        }

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
        var isActualAuthor = await _dbContext.Books.IsActualAuthor(bookId, _currentUser.Id);

        if (!isActualAuthor)
        {
            return;
        }

        var oldCoverId = await _dbContext
            .Books
            .Where(b => b.Id == bookId)
            .Select(b => b.CoverId)
            .FirstAsync();

        await _bookCover.DeleteCover(oldCoverId);

        await _dbContext.Books
            .Where(b => b.Id == bookId)
            .ExecuteDeleteAsync();
    }
}
