namespace Wheelingful.BLL.Models.Requests;

public class UpdateChapterTextRequest
{
    public int ChapterId { get; set; }
    public int BookId { get; set; }
    public required string Text { get; set; }
}
