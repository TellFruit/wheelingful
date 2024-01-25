using Microsoft.EntityFrameworkCore;
using Wheelingful.Core.Contracts.Auth;
using Wheelingful.Core.Contracts.Books;
using Wheelingful.Core.DTO.Books;
using Wheelingful.Core.Extensions;
using Wheelingful.Data.DbContexts;
using Wheelingful.Data.Entities;

namespace Wheelingful.Core.Services.Books;

internal class BookAuthorService(
    IBookCoverManager bookCover, 
    ICurrentUser currentUser, 
    WheelingfulDbContext dbContext) : IBookAuthorService
{
    public async Task CreateBook(NewBookModel model)
    {
        model.AuthorId = currentUser.Id;

        model.CoverId = await bookCover.UploadCover(model.CoverBase64, model.Title, model.AuthorId);

        var newBook = new Book
        {
            Title = model.Title,
            Description = model.Description,
            Category = model.Category,
            Status = model.Status,
            CoverId = model.CoverId,
        };

        var author = dbContext.Users
            .First(u => u.Id == model.AuthorId);

        newBook.Users.Add(author);

        dbContext.Add(newBook);

        await dbContext.SaveChangesAsync();

    }

    public async Task UpdateBook(UpdatedBookModel model)
    {
        var isActualAuthor = await dbContext.Books.IsActualAuthor(model.Id, currentUser.Id);

        if (!isActualAuthor)
        {
            return;
        }

        var book = await dbContext.Books.FirstAsync(b => b.Id == model.Id);

        if (model.CoverBase64 != null)
        {
            model.CoverId = await bookCover.UpdateCover(book.CoverId, model.CoverBase64, model.Title, currentUser.Id);
        }

        book.Title = model.Title;
        book.Description = model.Description;
        book.Category = model.Category;
        book.Status = model.Status;
        book.CoverId = model.CoverId ?? book.CoverId;

        dbContext.Update(book);

        await dbContext.SaveChangesAsync();
    }

    public async Task DeleteBook(int bookId)
    {
        var isActualAuthor = await dbContext.Books.IsActualAuthor(bookId, currentUser.Id);

        if (!isActualAuthor)
        {
            return;
        }

        var oldCoverId = await dbContext
            .Books
            .Where(b => b.Id == bookId)
            .Select(b => b.CoverId)
            .FirstAsync();

        await bookCover.DeleteCover(oldCoverId);

        await dbContext.Books
            .Where(b => b.Id == bookId)
            .ExecuteDeleteAsync();
    }
}
