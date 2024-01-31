using Wheelingful.BLL.Models.Requests.General;

namespace Wheelingful.BLL.Models.Requests;

public class FetchBookPaginationRequest : FetchPaginationRequest
{
    public string? AuthorId { get; set; }

    public FetchBookPaginationRequest(string? authorId, int? pageNumber, int? pageSize) : base(pageNumber, pageSize)
    {
        AuthorId = authorId;
    }
}
