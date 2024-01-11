using Microsoft.EntityFrameworkCore;

namespace Wheelingful.Data.DbContexts;

internal class WheelingfulDbContext : DbContext
{
    public WheelingfulDbContext(DbContextOptions<WheelingfulDbContext> options) 
        : base(options) { }
}
