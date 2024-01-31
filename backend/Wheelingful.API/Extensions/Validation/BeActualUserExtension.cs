using Microsoft.EntityFrameworkCore;
using Wheelingful.DAL.Entities;

namespace Wheelingful.API.Extensions.Validation;

public static class BeActualUserExtension
{
    public static Task<bool> BeActualUser(this DbSet<AppUser> set, string userId)
    {
        return set.AnyAsync(u => u.Id == userId);
    }
}
