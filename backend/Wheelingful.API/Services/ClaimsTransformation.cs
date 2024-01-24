using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Wheelingful.Core.Contracts.Auth;
using Wheelingful.Data.Entities;

namespace Wheelingful.API.Services;

internal class ClaimsTransformation : IClaimsTransformation
{
    private readonly ICurrentUser _currentUser;
    private readonly UserManager<AppUser> _userManager;

    public ClaimsTransformation(ICurrentUser currentUser, UserManager<AppUser> userManager)
    {
        _currentUser = currentUser;
        _userManager = userManager;
    }

    public async Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
    {
        var found = await _userManager.GetUserAsync(principal);

        ArgumentNullException.ThrowIfNull(found);

        _currentUser.Id = found.Id;

        var newPrincipal = principal.Clone();

        var claimsIdentity = principal.Identity as ClaimsIdentity;

        ArgumentNullException.ThrowIfNull(claimsIdentity);

        var roles = await _userManager.GetRolesAsync(found);

        foreach (var role in roles) 
        {
            claimsIdentity.AddClaim(new Claim(ClaimTypes.Role, role));
        }

        return newPrincipal;
    }
}
