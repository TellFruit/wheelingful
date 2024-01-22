using Wheelingful.Core.Contracts.Auth;
using Wheelingful.Core.Contracts.Books;
using Wheelingful.Core.DTO.Books;
using Wheelingful.Services.Extensions;

namespace Wheelingful.Services.Outer;

public class BookAuthorService : IBookAuthorService
{
    private readonly IBookManager _bookManager;
    private readonly ICurrentUser _currentUser;

    public BookAuthorService(IBookManager bookManager, ICurrentUser currentUser)
    {
        _bookManager = bookManager;
        _currentUser = currentUser;
    }

    public async Task CreateBook(NewBookModel model)
    {
        model.AuthorId = _currentUser.Id;

        await _bookManager.CreateBook(model);
    }

    public async Task UpdateBook(UpdatedBookModel model)
    {
        var check = await _bookManager.IsActualAuthor(model.Id, _currentUser.Id);

        if (check is false)
        {
            return;
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

        await _bookManager.DeleteBook(bookId);
    }
}
