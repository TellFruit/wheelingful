namespace Wheelingful.BLL.Models.Responses;

public class FetchReviewResponse
{
    public int BookId { get; set; }
    public required string UserId { get; set; }
    public required string UserName { get; set; }
    public required string Title { get; set; }
    public required string Text { get; set; }
    public int Score { get; set; }
}
