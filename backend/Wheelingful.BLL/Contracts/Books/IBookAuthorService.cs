using Wheelingful.BLL.Models.Books;

namespace Wheelingful.BLL.Contracts.Books;

public interface IBookAuthorService
{
    Task CreateBook(NewBookModel model);
    Task UpdateBook(UpdatedBookModel model);
    Task DeleteBook(int bookId);
}
