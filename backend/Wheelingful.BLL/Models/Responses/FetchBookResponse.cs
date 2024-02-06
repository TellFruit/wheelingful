namespace Wheelingful.BLL.Models.Responses;

public class FetchBookResponse
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public required string Description { get; set; }
    public required string Category { get; set; }
    public required string Status { get; set; }
    public required string CoverUrl { get; set; }
}
