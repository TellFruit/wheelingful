namespace Wheelingful.BLL.Models.Requests;

public class CreateReviewRequest
{
    public int BookId { get; set; }
    public required string Title { get; set; }
    public required string Text { get; set; }
    public int Score { get; set; }
}
