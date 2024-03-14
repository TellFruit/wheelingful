using Wheelingful.BLL.Models.Requests;

namespace Wheelingful.API.Models;

public class FetchReviewsByCurrentUserRequest : FetchReviewPaginationRequest
{
    public FetchReviewsByCurrentUserRequest(int? pageNumber, int? pageSize) 
        : base(null, true, pageNumber, pageSize) { }
}
