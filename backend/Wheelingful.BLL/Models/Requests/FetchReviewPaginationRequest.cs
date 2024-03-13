using Wheelingful.BLL.Models.Requests.General;

namespace Wheelingful.BLL.Models.Requests;

public class FetchReviewPaginationRequest : FetchPaginationRequest
{
    public int? BookId { get; set; }
    public bool? DoFetchByCurrentUser { get; set; }

    public FetchReviewPaginationRequest(int? bookId, int? pageNumber, int? pageSize, bool? doFetchByCurrentUser)
        : base(pageNumber, pageSize)
    {
        BookId = bookId;
        DoFetchByCurrentUser = doFetchByCurrentUser;
    }
}
