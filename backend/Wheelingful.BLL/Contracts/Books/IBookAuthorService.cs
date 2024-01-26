using Wheelingful.BLL.Models.Requests;

namespace Wheelingful.BLL.Contracts.Books;

public interface IBookAuthorService
{
    Task CreateBook(CreateBookRequest model);
    Task UpdateBook(UpdateBookModel model);
    Task DeleteBook(DeleteBookRequest model);
}
