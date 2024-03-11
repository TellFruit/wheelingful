using Microsoft.AspNetCore.Identity;
using Wheelingful.DAL.Entities.Abstract;

namespace Wheelingful.DAL.Entities;

public sealed class AppUser : IdentityUser, IBaseTimestamp
{
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    public List<Book> Books { get; set; } = [];
    public List<Review> Reviews { get; set; } = [];
}
