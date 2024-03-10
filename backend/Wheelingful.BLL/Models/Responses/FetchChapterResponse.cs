namespace Wheelingful.BLL.Models.Responses;

public class FetchChapterResponse
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public required string Text { get; set; }
    public int? PreviousChapterId { get; set; }
    public int? NextChapterId { get; set; }
}
