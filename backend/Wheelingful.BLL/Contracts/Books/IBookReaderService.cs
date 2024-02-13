using Wheelingful.BLL.Models.Requests;
using Wheelingful.BLL.Models.Responses;
using Wheelingful.BLL.Models.Responses.Generic;

namespace Wheelingful.BLL.Contracts.Books;

public interface IBookReaderService
{
    Task<FetchPaginationResponse<FetchBookResponse>> GetBooks(FetchBookPaginationRequest request);
    Task<FetchBookResponse> GetBook(FetchBookRequest request);
}
