using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Wheelingful.DAL.Entities;
using Wheelingful.DAL.Extensions;

namespace Wheelingful.DAL.DbContexts;

public class WheelingfulDbContext : IdentityDbContext<AppUser>
{
    public DbSet<Book> Books { get; set; }
    public DbSet<Chapter> Chapters { get; set; }
    public DbSet<Review> Reviews { get; set; }

    public WheelingfulDbContext(DbContextOptions<WheelingfulDbContext> options) 
        : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.BuildBook();
        modelBuilder.BuildChapter();
        modelBuilder.BuildReview();

        base.OnModelCreating(modelBuilder);

        modelBuilder.SeedRoles();
        modelBuilder.SeedUsers();
        modelBuilder.SeedAssignRoles();
    }
}
