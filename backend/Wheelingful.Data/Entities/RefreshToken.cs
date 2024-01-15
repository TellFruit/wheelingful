using Microsoft.AspNetCore.Identity;
using Wheelingful.Data.Entities.Abstract;

namespace Wheelingful.Data.Entities;

internal sealed class RefreshToken : BaseEntity
{
    public required string Signature { get; set; }
    public required DateTime ExpiresAt { get; set; }
    public required string UserId { get; set; }

    public bool IsValid => DateTime.UtcNow <= ExpiresAt;

    public User? User { get; set; }
}
