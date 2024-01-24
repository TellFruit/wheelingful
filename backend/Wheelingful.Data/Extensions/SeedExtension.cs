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
                Id = "3f6d7281-1c57-4fd9-895b-9c76f14f3eae",
                Name = nameof(UserRoleEnum.Reader),
                NormalizedName = nameof(UserRoleEnum.Reader).ToUpper()
            },
            new IdentityRole
            {
                Id = "e8f6d91a-9d3b-4a9a-bc32-6b03bf8c25c4",
                Name = nameof(UserRoleEnum.Author),
                NormalizedName = nameof(UserRoleEnum.Author).ToUpper()
            },
            new IdentityRole
            {
                Id = "7b92af56-2d6e-4c8b-aae5-4f843faa1c79",
                Name = nameof(UserRoleEnum.Admin),
                NormalizedName = nameof(UserRoleEnum.Admin).ToUpper()
            }
        );
    }

    public static void SeedUsers(this ModelBuilder builder)
    {
        var creationDate = new DateTime(2024, 1, 19, 17, 50, 58, 470, DateTimeKind.Utc).AddTicks(6088);

        var adminUser = new AppUser
        {
            Id = "ff38c4e9-8a0a-4a95-bb82-1f7685db3c59",
            UserName = "admin@wheelingful.com",
            NormalizedUserName = "ADMIN@WHEELINGFUL.COM",
            Email = "admin@wheelingful.com",
            NormalizedEmail = "ADMIN@WHEELINGFUL.COM",
            EmailConfirmed = true,
            SecurityStamp = "WRRZSMKR6S3WSKHAT32VF7EBZV6KJTFL",
            ConcurrencyStamp = "cb7a7e3f-94cf-4e20-8e27-1ab264497106",
            PasswordHash = "AQAAAAIAAYagAAAAECgHupnB8wkJMaI20CCVFYYCvCAFFDsvb0eDQR0+GuK3KjAFYTbRynrrGFk/3NVwIA==",
            CreatedAt = creationDate,
            UpdatedAt = creationDate
        };

        builder.Entity<AppUser>().HasData(adminUser);
    }

    public static void SeedAssignRoles(this ModelBuilder builder)
    {
        builder.Entity<IdentityUserRole<string>>().HasData(
            new IdentityUserRole<string>
            {
                UserId = "ff38c4e9-8a0a-4a95-bb82-1f7685db3c59",
                RoleId = "7b92af56-2d6e-4c8b-aae5-4f843faa1c79"
            }
        );
    }
}
