using Wheelingful.BLL.Models.Requests.General;

namespace Wheelingful.BLL.Models.Requests;

public class CountBookPaginationPagesRequest : CountPagesRequest
{
    public bool? DoFetchByCurrentUser { get; set; }

    public CountBookPaginationPagesRequest(bool? doFetchByCurrentUser, int? pageSize) : base(pageSize)
    {
        DoFetchByCurrentUser = doFetchByCurrentUser;
    }
}
