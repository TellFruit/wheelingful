namespace Wheelingful.BLL.Models.Requests;

public class CreateChapterRequest
{
    public required string Title { get; set; }
    public int BookId { get; set; }
}
