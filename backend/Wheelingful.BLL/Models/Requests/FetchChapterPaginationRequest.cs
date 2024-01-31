using Wheelingful.BLL.Models.Requests.General;

namespace Wheelingful.BLL.Models.Requests;

public class FetchChapterPaginationRequest : FetchPaginationRequest
{
    public int BookId { get; set; }

    public FetchChapterPaginationRequest(int bookId, int? pageNumber, int? pageSize) 
        : base(pageNumber, pageSize)
    {
        BookId = bookId;
    }
}
