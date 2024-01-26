using Wheelingful.BLL.Models.Books;

namespace Wheelingful.BLL.Contracts.Books;

public interface IBookAuthorService
{
    Task CreateBook(CreateBookModel model);
    Task UpdateBook(UpdateBookModel model);
    Task DeleteBook(DeleteBookModel model);
}
