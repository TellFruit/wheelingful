using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Wheelingful.Data.Extensions;

namespace Wheelingful.Data.DbContexts;

internal class WheelingfulDbContext : IdentityDbContext<IdentityUser>
{
    public WheelingfulDbContext(DbContextOptions<WheelingfulDbContext> options) 
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.SeedRoles();
        modelBuilder.SeedUsers();
        modelBuilder.SeedAssignRoles();
    }
}
