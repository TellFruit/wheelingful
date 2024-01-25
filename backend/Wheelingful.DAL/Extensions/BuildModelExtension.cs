using Microsoft.EntityFrameworkCore;
using Wheelingful.DAL.Entities;

namespace Wheelingful.DAL.Extensions;

internal static class BuildModelExtension
{
    public static void BuildModels(this ModelBuilder builder)
    {
        builder.Entity<AppUser>()
            .HasMany(u => u.Books)
            .WithMany(b => b.Users)
            .UsingEntity("Authorship");

        builder.Entity<Book>()
            .Property(b => b.Title)
            .HasMaxLength(255);

        builder.Entity<Book>()
            .Property(b => b.Description)
            .HasMaxLength(1000);

        builder.Entity<Book>()
            .Property(b => b.CoverId)
            .HasMaxLength(24);
    }
}
