using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Wheelingful.Data.DbContexts;

internal class WheelingfulDbContext : IdentityDbContext<IdentityUser>
{
    public WheelingfulDbContext(DbContextOptions<WheelingfulDbContext> options) 
        : base(options) { }
}
