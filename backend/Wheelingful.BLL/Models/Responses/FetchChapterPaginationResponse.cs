namespace Wheelingful.BLL.Models.Responses;

public class FetchChapterPaginationResponse
{
    public int PageCount { get; set; }
    public List<FetchChapterPropsResponse> Chapters { get; set; } = [];
}
