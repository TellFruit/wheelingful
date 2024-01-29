using Wheelingful.BLL.Constants;

namespace Wheelingful.BLL.Models.Requests.General;

public class FetchPaginationRequest
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }

    public FetchPaginationRequest(int? pageNumber, int? pageSize)
    {
        PageNumber = pageNumber ?? PaginationConstants.DefaultPageNumber;
        PageSize = pageSize ?? PaginationConstants.DefaultPageSize;
    }
}
