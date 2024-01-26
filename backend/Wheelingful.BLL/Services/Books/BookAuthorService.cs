using Microsoft.EntityFrameworkCore;
using Wheelingful.BLL.Contracts.Auth;
using Wheelingful.BLL.Contracts.Books;
using Wheelingful.BLL.Models.Books;
using Wheelingful.DAL.DbContexts;
using Wheelingful.DAL.Entities;

namespace Wheelingful.BLL.Services.Books;

internal class BookAuthorService(
    IBookCoverManager bookCover, 
    ICurrentUser currentUser, 
    WheelingfulDbContext dbContext) : IBookAuthorService
{
    public async Task CreateBook(CreateBookModel model)
    {
        var coverId = await bookCover.UploadCover(model.CoverBase64, model.Title, currentUser.Id);

        var newBook = new Book
        {
            Title = model.Title,
            Description = model.Description,
            Category = model.Category,
            Status = model.Status,
            CoverId = coverId,
        };

        var author = dbContext.Users
            .First(u => u.Id == currentUser.Id);

        newBook.Users.Add(author);

        dbContext.Add(newBook);

        await dbContext.SaveChangesAsync();

    }

    public async Task UpdateBook(UpdateBookModel model)
    {
        var book = await dbContext.Books.FirstAsync(b => b.Id == model.Id);

        string? coverId = book.CoverId;
        if (model.CoverBase64 != null)
        {
            coverId = await bookCover.UpdateCover(book.CoverId, model.CoverBase64, model.Title, currentUser.Id);
        }

        book.Title = model.Title;
        book.Description = model.Description;
        book.Category = model.Category;
        book.Status = model.Status;
        book.CoverId = coverId;

        dbContext.Update(book);

        await dbContext.SaveChangesAsync();
    }

    public async Task DeleteBook(DeleteBookModel model)
    {
        var coverId = await dbContext
            .Books
            .Where(b => b.Id == model.Id)
            .Select(b => b.CoverId)
            .FirstAsync();

        await bookCover.DeleteCover(coverId);

        await dbContext.Books
            .Where(b => b.Id == model.Id)
            .ExecuteDeleteAsync();
    }
}
