namespace Wheelingful.BLL.Models.Responses;

public class FetchBookPaginationResponse
{
    public int PageCount { get; set; }
    public List<FetchBookResponse> Books { get; set; } = [];
}
