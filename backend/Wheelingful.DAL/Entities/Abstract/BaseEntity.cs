namespace Wheelingful.DAL.Entities.Abstract;

public abstract class BaseEntity : IBaseTimestamp
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}
