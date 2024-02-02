using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Wheelingful.BLL.Contracts.Auth;
using Wheelingful.DAL.Entities;

namespace Wheelingful.API.Services;

internal class ClaimsTransformation(ICurrentUser currentUser, UserManager<AppUser> userManager) : IClaimsTransformation
{
    public async Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
    {
        var found = await userManager.GetUserAsync(principal);

        if (found == null)
        {
            return principal;
        }

        currentUser.Id = found.Id;

        var newPrincipal = principal.Clone();

        var claimsIdentity = principal.Identity as ClaimsIdentity;

        if (claimsIdentity == null)
        {
            return principal;
        }

        var roles = await userManager.GetRolesAsync(found);

        foreach (var role in roles) 
        {
            claimsIdentity.AddClaim(new Claim(ClaimTypes.Role, role));
        }

        return newPrincipal;
    }
}
