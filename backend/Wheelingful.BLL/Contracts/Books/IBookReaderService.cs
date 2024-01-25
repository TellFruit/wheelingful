using Wheelingful.BLL.Models.Books;
using Wheelingful.BLL.Models.General;

namespace Wheelingful.BLL.Contracts.Books;

public interface IBookReaderService
{
    Task<List<FetchBookModel>> GetBooks(FetchRequest filter);
}
