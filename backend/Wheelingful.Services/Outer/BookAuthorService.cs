using Microsoft.EntityFrameworkCore;
using Wheelingful.Core.Contracts.Auth;
using Wheelingful.Core.Contracts.Books;
using Wheelingful.Core.DTO.Books;
using Wheelingful.Services.Extensions;

namespace Wheelingful.Services.Outer;

internal class BookAuthorService : IBookAuthorService
{
    private readonly IBookCoverManager _bookCover;
    private readonly IBookManager _bookManager;
    private readonly ICurrentUser _currentUser;

    public BookAuthorService(IBookManager bookManager, IBookCoverManager bookCover, ICurrentUser currentUser)
    {
        _bookCover = bookCover;
        _bookManager = bookManager;
        _currentUser = currentUser;
    }

    public async Task CreateBook(NewBookModel model)
    {
        model.AuthorId = _currentUser.Id;

        model.CoverId = await _bookCover.UploadCover(model.CoverBase64, model.Title, model.AuthorId);

        await _bookManager.CreateBook(model);
    }

    public async Task UpdateBook(UpdatedBookModel model)
    {
        var check = await _bookManager.IsActualAuthor(model.Id, _currentUser.Id);

        if (check is false)
        {
            return;
        }

        if (model.CoverBase64 != null)
        {
            var oldCoverId = await _bookManager
                .GetBooks()
                .Where(b => b.Id == model.Id)
                .Select(b => b.CoverId)
                .FirstAsync();

            model.CoverId = await _bookCover.UpdateCover(oldCoverId, model.CoverBase64, model.Title, _currentUser.Id);
        }
        
        await _bookManager.UpdateBook(model);
    }

    public async Task DeleteBook(int bookId)
    {
        var check = await _bookManager.IsActualAuthor(bookId, _currentUser.Id);

        if (check is false)
        {
            return;
        }

        var oldCoverId = await _bookManager
            .GetBooks()
            .Where(b => b.Id == bookId)
            .Select(b => b.CoverId)
            .FirstAsync();

        await _bookCover.DeleteCover(oldCoverId);

        await _bookManager.DeleteBook(bookId);
    }
}
