using Wheelingful.BLL.Models.Requests;
using Wheelingful.BLL.Models.Responses;

namespace Wheelingful.BLL.Contracts.Books;

public interface IBookReaderService
{
    Task<List<FetchBookResponse>> GetBooks(FetchBookPaginationRequest request);
    Task<FetchBookResponse> GetBook(FetchBookRequest request);
}
