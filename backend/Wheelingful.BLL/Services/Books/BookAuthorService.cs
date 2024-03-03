using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using Wheelingful.BLL.Contracts.Auth;
using Wheelingful.BLL.Contracts.Books;
using Wheelingful.BLL.Contracts.Chapters;
using Wheelingful.BLL.Models.Requests;
using Wheelingful.DAL.Contracts;
using Wheelingful.DAL.DbContexts;
using Wheelingful.DAL.Entities;
using Wheelingful.DAL.Helpers;

namespace Wheelingful.BLL.Services.Books;

internal class BookAuthorService(
    IBookCoverService bookCover,
    IChapterTextService textService,
    ICurrentUser currentUser,
    ILogger<BookAuthorService> logger,
    ICacheService cacheService,
    WheelingfulDbContext dbContext) : IBookAuthorService
{
    public async Task CreateBook(CreateBookRequest request)
    {
        logger.LogInformation("User {userId} created a book: {request}",
            currentUser.Id, JsonSerializer.Serialize(request));

        var newBook = new Book
        {
            Title = request.Title,
            Description = request.Description,
            Category = request.Category,
            Status = request.Status,
        };

        var author = dbContext.Users
            .AsTracking()
            .First(u => u.Id == currentUser.Id);

        newBook.Users.Add(author);

        dbContext.Add(newBook);

        await dbContext.SaveChangesAsync();

        var coverId = await bookCover.UploadCover(request.CoverBase64, newBook.Id, currentUser.Id);

        newBook.CoverId = coverId;

        await dbContext.SaveChangesAsync();

        var prefix = nameof(Book).ToCachePrefix();

        await cacheService.RemoveByPrefix(prefix);
    }

    public async Task UpdateBook(UpdateBookRequest request)
    {
        logger.LogInformation("User {userId} updated the book: {request}",
            currentUser.Id, JsonSerializer.Serialize(request));

        var book = await dbContext.Books.FirstAsync(b => b.Id == request.BookId);

        var coverId = book.CoverId;
        if (request.CoverBase64 != null)
        {
            coverId = await bookCover.UpdateCover(book.CoverId, request.CoverBase64, book.Id, currentUser.Id);
        }

        book.Title = request.Title;
        book.Description = request.Description;
        book.Category = request.Category;
        book.Status = request.Status;
        book.CoverId = coverId;
        book.UpdatedAt = DateTime.UtcNow;

        dbContext.Update(book);

        await dbContext.SaveChangesAsync();

        var prefix = nameof(Book).ToCachePrefix(request.BookId);

        await cacheService.RemoveByPrefix(prefix);
    }

    public async Task DeleteBook(DeleteBookRequest request)
    {
        logger.LogInformation("User {userId} deleted the book: {request}",
            currentUser.Id, JsonSerializer.Serialize(request));

        textService.DeleteByBook(request.BookId);

        var coverId = await dbContext
            .Books
            .Where(b => b.Id == request.BookId)
            .Select(b => b.CoverId)
            .FirstAsync();

        await bookCover.DeleteCover(coverId);

        await dbContext.Books
            .Where(b => b.Id == request.BookId)
            .ExecuteDeleteAsync();

        var prefix = nameof(Book).ToCachePrefix(request.BookId);

        await cacheService.RemoveByPrefix(prefix);
    }
}
