using Wheelingful.DAL.Entities.Abstract;

namespace Wheelingful.DAL.Entities;

public class Chapter : BaseEntity
{
    public required string Title { get; set; }
    public int BookId { get; set; }

    public Book Book { get; set; } = null!;
}
