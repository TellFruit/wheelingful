using Wheelingful.DAL.Entities.Abstract;

namespace Wheelingful.DAL.Entities;

public class Review : IBaseTimestamp
{
    public int BookId { get; set; }
    public required string UserId { get; set; }
    public required string Title { get; set; }
    public required string Text { get; set; }
    public int Score { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;

    public Book Book { get; set; } = null!;
    public AppUser User { get; set; } = null!;
}
