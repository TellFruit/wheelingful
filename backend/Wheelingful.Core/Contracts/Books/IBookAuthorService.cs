using Wheelingful.Core.DTO.Books;

namespace Wheelingful.Core.Contracts.Books;

public interface IBookAuthorService
{
    Task CreateBook(CreateBookRequest request);
    Task UpdateBook(UpdateBookRequest request);
    Task DeleteBook(DeleteBookRequest request);
}
