using Wheelingful.BLL.Models.Requests.General;

namespace Wheelingful.BLL.Models.Requests;
public class CountChapterPaginationPagesRequest : CountPagesRequest
{
    public int BookId { get; set; }

    public CountChapterPaginationPagesRequest(int bookId, int? pageSize) : base(pageSize)
    {
        BookId = bookId;
    }
}
