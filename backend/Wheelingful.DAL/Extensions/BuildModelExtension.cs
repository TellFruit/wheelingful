using Microsoft.EntityFrameworkCore;
using Wheelingful.DAL.Entities;

namespace Wheelingful.DAL.Extensions;

internal static class BuildModelExtension
{
    public static void BuildBook(this ModelBuilder builder)
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

    public static void BuildChapter(this ModelBuilder builder)
    {
        builder.Entity<Chapter>()
            .Property(b => b.Title)
            .HasMaxLength(255);
    }

    public static void BuildReview(this ModelBuilder builder)
    {
        builder.Entity<Review>()
            .HasKey(r => new { r.BookId, r.UserId });

        builder.Entity<Review>()
            .Property(r => r.Title)
            .HasMaxLength(255);

        builder.Entity<Review>()
            .Property(r => r.Text)
            .HasMaxLength(1000);

        builder.Entity<Review>()
            .HasOne(r => r.Book)
            .WithMany(b => b.Reviews)
            .HasForeignKey(r => r.BookId);

        builder.Entity<Review>()
            .HasOne(r => r.User)
            .WithMany(u => u.Reviews)
            .HasForeignKey(r => r.UserId);
    }
}
