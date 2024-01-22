using Wheelingful.Core.DTO.Books;

namespace Wheelingful.Core.Contracts.Books;

public interface IBookAuthorService
{
    Task CreateBook(NewBookModel model);
    Task UpdateBook(UpdatedBookModel model);
    Task DeleteBook(int bookId);
}
