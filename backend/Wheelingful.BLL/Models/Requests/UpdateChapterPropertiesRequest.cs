namespace Wheelingful.BLL.Models.Requests;

public class UpdateChapterPropertiesRequest
{
    public int Id { get; set; }
    public int BookId { get; set; }
    public required string Title { get; set; }
}
