namespace Wheelingful.BLL.Models.Requests;

public class UpdateChapterRequest
{
    public int ChapterId { get; set; }
    public int BookId { get; set; }
    public required string Title { get; set; }
    public string? Text { get; set; } = null!;
}
