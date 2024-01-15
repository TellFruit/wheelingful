using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Wheelingful.Data.Entities;
using Wheelingful.Data.Extensions;

namespace Wheelingful.Data.DbContexts;

internal class WheelingfulDbContext : IdentityDbContext<User>
{
    public DbSet<RefreshToken> RefreshTokens { get; set; }

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
