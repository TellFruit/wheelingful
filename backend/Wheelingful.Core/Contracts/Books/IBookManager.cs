using Wheelingful.Core.DTO.Books;

namespace Wheelingful.Core.Contracts.Books;

public interface IBookManager
{
    Task CreateBook(NewBookModel model);
    IQueryable<ReadBookModel> GetBooks();
    Task UpdateBook(UpdatedBookModel model);
    Task DeleteBook(int bookId);
}
