using System.Diagnostics.CodeAnalysis;
using Wheelingful.BLL.Constants;

namespace Wheelingful.BLL.Models.Requests.General;

public class FetchPaginationRequest
{
    [NotNull]
    public int? PageNumber { get; set; }
    [NotNull]
    public int? PageSize { get; set; }

    public FetchPaginationRequest(int? pageNumber, int? pageSize)
    {
        PageNumber = pageNumber ?? PaginationConstants.DefaultPageNumber;
        PageSize = pageSize ?? PaginationConstants.DefaultPageSize;
    }
}
