using Microsoft.AspNetCore.Identity;

namespace Wheelingful.Data.Entities;

internal sealed class User : IdentityUser
{
    public ICollection<RefreshToken>? RefreshTokens { get; set; }
}
