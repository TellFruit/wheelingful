using Wheelingful.BLL.Models.Requests;

namespace Wheelingful.API.Models;

public class FetchReviewsByBookRequest : FetchReviewPaginationRequest
{
    public new int BookId { get; set; }

    public FetchReviewsByBookRequest(int bookId, int? pageNumber, int? pageSize) 
        : base(bookId, null, pageNumber, pageSize) { }
}
