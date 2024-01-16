using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Wheelingful.Core.Enums;

namespace Wheelingful.Data.Extensions;

internal static class SeedExtension
{
    public static void SeedRoles(this ModelBuilder builder)
    {

        builder.Entity<IdentityRole>().HasData(
            new IdentityRole
            {
                Id = "1",
                Name = nameof(UserRoleEnum.Admin),
                NormalizedName = nameof(UserRoleEnum.Admin).ToUpper()
            },
            new IdentityRole
            {
                Id = "2",
                Name = nameof(UserRoleEnum.Reader),
                NormalizedName = nameof(UserRoleEnum.Reader).ToUpper()
            },
            new IdentityRole
            {
                Id = "3",
                Name = nameof(UserRoleEnum.Author),
                NormalizedName = nameof(UserRoleEnum.Author).ToUpper()
            }
        );
    }

    public static void SeedUsers(this ModelBuilder builder)
    {
        var passwordHasher = new PasswordHasher<IdentityUser>();

        builder.Entity<IdentityUser>().HasData(
            new IdentityUser
            {
                Id = "ff38c4e9-8a0a-4a95-bb82-1f7685db3c59",
                UserName = "admin@wheelingful.com",
                NormalizedUserName = "ADMIN@WHEELINGFUL.COM",
                Email = "admin@wheelingful.com",
                NormalizedEmail = "ADMIN@WHEELINGFUL.COM",
                EmailConfirmed = true,
                PasswordHash = string.Empty,
                SecurityStamp = string.Empty,
                ConcurrencyStamp = string.Empty,
            }
        );
    }

    public static void SeedAssignRoles(this ModelBuilder builder)
    {
        builder.Entity<IdentityUserRole<string>>().HasData(
            new IdentityUserRole<string>
            {
                UserId = "ff38c4e9-8a0a-4a95-bb82-1f7685db3c59",
                RoleId = "1"
            }
        );
    }
}
