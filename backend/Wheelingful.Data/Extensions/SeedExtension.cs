using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Wheelingful.Core.Enums;
using Wheelingful.Data.Entities;

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
        var passwordHasher = new PasswordHasher<User>();

        builder.Entity<User>().HasData(
            new User
            {
                Id = "1",
                UserName = "admin@wheelingful.com",
                NormalizedUserName = "ADMIN@WHEELINGFUL.COM",
                Email = "admin@wheelingful.com",
                NormalizedEmail = "ADMIN@WHEELINGFUL.COM",
                EmailConfirmed = true,
                PasswordHash = passwordHasher.HashPassword(null, "#SYSADMIN123"),
                SecurityStamp = string.Empty
            }
        );
    }

    public static void SeedAssignRoles(this ModelBuilder builder)
    {
        builder.Entity<IdentityUserRole<string>>().HasData(
            new IdentityUserRole<string>
            {
                UserId = "1",
                RoleId = "1"
            }
        );
    }
}
