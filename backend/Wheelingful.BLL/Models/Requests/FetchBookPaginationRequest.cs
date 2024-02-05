using Wheelingful.BLL.Models.Requests.General;

namespace Wheelingful.BLL.Models.Requests;

public class FetchBookPaginationRequest : FetchPaginationRequest
{
    public bool? DoFetchByCurrentUser { get; set; }

    public FetchBookPaginationRequest(bool? doFetchByCurrentUser, int? pageNumber, int? pageSize) 
        : base(pageNumber, pageSize)
    {
        DoFetchByCurrentUser = doFetchByCurrentUser;
    }
}
