namespace Wheelingful.DAL.Entities;

public class Review
{
    public int BookId { get; set; }
    public required string UserId { get; set; }
    public required string Title { get; set; }
    public required string Text { get; set; }
    public int Score { get; set; }

    public Book Book { get; set; } = null!;
    public AppUser User { get; set; } = null!;
}
