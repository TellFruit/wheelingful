using Microsoft.EntityFrameworkCore;
using Wheelingful.BLL.Contracts.Auth;
using Wheelingful.BLL.Contracts.Books;
using Wheelingful.BLL.Models.Requests;
using Wheelingful.DAL.DbContexts;
using Wheelingful.DAL.Entities;

namespace Wheelingful.BLL.Services.Books;

internal class BookAuthorService(
    IBookCoverService bookCover, 
    ICurrentUser currentUser, 
    WheelingfulDbContext dbContext) : IBookAuthorService
{
    public async Task CreateBook(CreateBookRequest request)
    {
        var coverId = await bookCover.UploadCover(request.CoverBase64, request.Title, currentUser.Id);

        var newBook = new Book
        {
            Title = request.Title,
            Description = request.Description,
            Category = request.Category,
            Status = request.Status,
            CoverId = coverId,
        };

        var author = dbContext.Users
            .AsTracking()
            .First(u => u.Id == currentUser.Id);

        newBook.Users.Add(author);

        dbContext.Add(newBook);

        await dbContext.SaveChangesAsync();

    }

    public async Task UpdateBook(UpdateBookRequest request)
    {
        var book = await dbContext.Books.FirstAsync(b => b.Id == request.Id);

        string? coverId = book.CoverId;
        if (request.CoverBase64 != null)
        {
            coverId = await bookCover.UpdateCover(book.CoverId, request.CoverBase64, request.Title, currentUser.Id);
        }

        book.Title = request.Title;
        book.Description = request.Description;
        book.Category = request.Category;
        book.Status = request.Status;
        book.CoverId = coverId;
        book.UpdatedAt = DateTime.UtcNow;

        dbContext.Update(book);

        await dbContext.SaveChangesAsync();
    }

    public async Task DeleteBook(DeleteBookRequest request)
    {
        var coverId = await dbContext
            .Books
            .Where(b => b.Id == request.Id)
            .Select(b => b.CoverId)
            .FirstAsync();

        await bookCover.DeleteCover(coverId);

        await dbContext.Books
            .Where(b => b.Id == request.Id)
            .ExecuteDeleteAsync();
    }
}
