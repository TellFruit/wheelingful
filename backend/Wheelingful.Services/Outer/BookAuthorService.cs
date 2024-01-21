using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Wheelingful.Core.Contracts.Books;
using Wheelingful.Core.DTO.Author.Abstract;
using Wheelingful.Core.DTO.Books;
using Wheelingful.Data.Entities;

namespace Wheelingful.Services.Outer;

public class BookAuthorService : IBookAuthorService
{
    private readonly IBookManager _bookManager;
    private readonly UserManager<AppUser> _userManager;

    public BookAuthorService(IBookManager bookManager, UserManager<AppUser> userManager)
    {
        _bookManager = bookManager;
        _userManager = userManager;
    }

    public async Task CreateBook(CreateBookRequest request)
    {
        var author = await _userManager.GetUserAsync(request.AuthorClaims);

        request.NewBook.AuthorId = author!.Id;

        await _bookManager.CreateBook(request.NewBook);
    }

    public async Task UpdateBook(UpdateBookRequest request)
    {
        var check = await IsActualAuthor(request, request.UpdatedBook.Id);

        if (check is false)
        {
            return;
        }

        await _bookManager.UpdateBook(request.UpdatedBook);
    }

    public async Task DeleteBook(DeleteBookRequest request)
    {
        var check = await IsActualAuthor(request, request.BookId);

        if (check is false)
        {
            return;
        }

        await _bookManager.DeleteBook(request.BookId);
    }

    private async Task<bool> IsActualAuthor(AuthorAuthorizeRequest request, int bookId)
    {
        var authorTask = _userManager.GetUserAsync(request.AuthorClaims);

        var bookToUpdate = await _bookManager
            .ReadBoooks()
            .FirstAsync(b => b.Id == bookId);

        var author = await authorTask;

        return bookToUpdate.AuthorId != author!.Id;
    }
}
