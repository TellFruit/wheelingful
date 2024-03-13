namespace Wheelingful.BLL.Models.Requests;

public class DeleteReviewRequest
{
    public int BookId { get; set; }
    public required string UserId { get; set; }
}
