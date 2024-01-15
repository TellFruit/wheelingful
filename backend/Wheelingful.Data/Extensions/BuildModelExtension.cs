using Microsoft.EntityFrameworkCore;
using Wheelingful.Data.Entities;

namespace Wheelingful.Data.Extensions;

internal static class BuildModelExtension
{
    public static void BuildModels(this ModelBuilder builder)
    {
        builder.Entity<User>()
            .HasMany(u => u.RefreshTokens)
            .WithOne(rt => rt.User)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<RefreshToken>()
            .HasOne(rt => rt.User)
            .WithMany(u => u.RefreshTokens)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);
    }
}
