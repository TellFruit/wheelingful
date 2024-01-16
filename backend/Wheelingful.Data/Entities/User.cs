using Microsoft.AspNetCore.Identity;

namespace Wheelingful.Data.Entities;

public sealed class User : IdentityUser
{
    public ICollection<RefreshToken>? RefreshTokens { get; set; }
}
