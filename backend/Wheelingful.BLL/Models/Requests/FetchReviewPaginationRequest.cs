using Wheelingful.BLL.Models.Requests.General;

namespace Wheelingful.BLL.Models.Requests;

public class FetchReviewPaginationRequest : FetchPaginationRequest
{
    public int? BookId { get; set; }
    public bool? DoFetchByCurrentUser { get; set; }

    public FetchReviewPaginationRequest(int? bookId, bool? doFetchByCurrentUser, int? pageNumber, int? pageSize)
        : base(pageNumber, pageSize)
    {
        BookId = bookId;
        DoFetchByCurrentUser = doFetchByCurrentUser;
    }
}
