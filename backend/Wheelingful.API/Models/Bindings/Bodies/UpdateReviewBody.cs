namespace Wheelingful.API.Models.Bindings.Bodies;

public class UpdateReviewBody
{
    public required string Title { get; set; }
    public required string Text { get; set; }
    public int Score { get; set; }
}
