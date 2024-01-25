using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Wheelingful.DAL.Entities;
using Wheelingful.DAL.Extensions;

namespace Wheelingful.DAL.DbContexts;

public class WheelingfulDbContext : IdentityDbContext<AppUser>
{
    public DbSet<Book> Books { get; set; }

    public WheelingfulDbContext(DbContextOptions<WheelingfulDbContext> options) 
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.BuildModels();

        base.OnModelCreating(modelBuilder);

        modelBuilder.SeedRoles();
        modelBuilder.SeedUsers();
        modelBuilder.SeedAssignRoles();
    }
}
