using Wheelingful.Core.Enums;
using Wheelingful.Data.Entities.Abstract;

namespace Wheelingful.Data.Entities;

public sealed class Book : BaseEntity, IBaseTimestamp
{
    public required string Title { get; set; }
    public required string Description { get; set; }
    public BookCategoryEnum Category { get; set; }
    public BookStatusEnum Status { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    public List<AppUser> Users { get; set; } = [];
}
