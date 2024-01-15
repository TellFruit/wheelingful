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
            .HasForeignKey(rt => rt.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<RefreshToken>()
            .HasOne(rt => rt.User)
            .WithMany(u => u.RefreshTokens)
            .HasForeignKey(rt => rt.UserId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);
    }
}
