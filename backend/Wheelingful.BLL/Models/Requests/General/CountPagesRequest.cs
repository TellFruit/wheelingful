using Wheelingful.BLL.Constants;

namespace Wheelingful.BLL.Models.Requests.General;

public class CountPagesRequest
{
    public int PageSize { get; set; }

    public CountPagesRequest(int? pageSize)
    { 
        PageSize = pageSize ?? PaginationConstants.DefaultPageSize; 
    }
}
