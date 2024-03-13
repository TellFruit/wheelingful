namespace Wheelingful.BLL.Models.Requests;

public class FetchReviewRequest
{
    public int BookId { get; set; }
    public required string UserId { get; set; }
}
