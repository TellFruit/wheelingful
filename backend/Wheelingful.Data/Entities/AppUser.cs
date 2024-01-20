using Microsoft.AspNetCore.Identity;
using Wheelingful.Data.Entities.Abstract;

namespace Wheelingful.Data.Entities;

public sealed class AppUser : IdentityUser, IBaseTimestamp
{
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    public List<Book> Books { get; set; } = [];
}
