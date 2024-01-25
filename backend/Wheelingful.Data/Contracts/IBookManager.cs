using Wheelingful.Data.Entities;

namespace Wheelingful.Data.Contracts.Books;

public interface IBookManager
{
    Task CreateBook(Book model);
    IQueryable<Book> GetBooks();
    Task UpdateBook(Book model);
    Task DeleteBook(int bookId);
}
