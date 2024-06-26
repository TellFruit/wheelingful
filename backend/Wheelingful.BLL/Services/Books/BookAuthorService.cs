﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Wheelingful.BLL.Contracts.Auth;
using Wheelingful.BLL.Contracts.Books;
using Wheelingful.BLL.Contracts.Chapters;
using Wheelingful.BLL.Models.Requests;
using Wheelingful.DAL.Contracts;
using Wheelingful.DAL.DbContexts;
using Wheelingful.DAL.Entities;
using Wheelingful.DAL.Helpers;

namespace Wheelingful.BLL.Services.Books;

public class BookAuthorService(
    IBookCoverService bookCover,
    IChapterTextService textService,
    ICurrentUser currentUser,
    ILogger<BookAuthorService> logger,
    ICacheService cacheService,
    WheelingfulDbContext dbContext) : IBookAuthorService
{
    public async Task CreateBook(CreateBookRequest request)
    {
        logger.LogInformation("User {UserId} created a book: {@Request}",
            currentUser.Id, request);

        var newBook = new Book
        {
            Title = request.Title,
            Description = request.Description,
            Category = request.Category,
            Status = request.Status,
            UserId = currentUser.Id,
        };

        dbContext.Add(newBook);

        await dbContext.SaveChangesAsync();

        var coverId = await bookCover.UploadCover(request.CoverBase64, newBook.Id, currentUser.Id);

        newBook.CoverId = coverId;

        await dbContext.SaveChangesAsync();

        var listPrefix = nameof(Book).ToCachePrefix();

        await cacheService.RemoveByPrefix(listPrefix);
    }

    public async Task UpdateBook(UpdateBookRequest request)
    {
        logger.LogInformation("User {UserId} updated the book: {@Request}",
            currentUser.Id, request);

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

        var entryById = nameof(Book).ToCachePrefix(request.BookId);

        await cacheService.RemoveByKey(entryById);

        var listPrefix = nameof(Book).ToCachePrefix();

        await cacheService.RemoveByPrefix(listPrefix);
    }

    public async Task DeleteBook(DeleteBookRequest request)
    {
        logger.LogInformation("User {UserId} deleted the book: {@Request}",
            currentUser.Id, request);

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

        var listPrefix = nameof(Book).ToCachePrefix();

        await cacheService.RemoveByPrefix(listPrefix);
    }
}
