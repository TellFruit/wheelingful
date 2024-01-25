using Wheelingful.DAL.Entities;

namespace Wheelingful.DAL.Contracts.Books;

public interface IBookManager
{
    Task CreateBook(Book model);
    IQueryable<Book> GetBooks();
    Task UpdateBook(Book model);
    Task DeleteBook(int bookId);
}
