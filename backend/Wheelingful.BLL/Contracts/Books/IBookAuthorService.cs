using Wheelingful.BLL.Models.Requests;

namespace Wheelingful.BLL.Contracts.Books;

public interface IBookAuthorService
{
    Task CreateBook(CreateBookRequest request);
    Task UpdateBook(UpdateBookRequest request);
    Task DeleteBook(DeleteBookRequest request);
}
