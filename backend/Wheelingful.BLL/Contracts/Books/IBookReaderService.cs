using Wheelingful.BLL.Models.Requests;
using Wheelingful.BLL.Models.Requests.General;
using Wheelingful.BLL.Models.Responses;

namespace Wheelingful.BLL.Contracts.Books;

public interface IBookReaderService
{
    Task<List<FetchBookResponse>> GetBooks(FetchPaginationRequest request);
    Task<FetchBookResponse> GetBook(FetchBookRequest request);
}
