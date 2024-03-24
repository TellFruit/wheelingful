using Microsoft.EntityFrameworkCore;
using Wheelingful.DAL.DbContexts;

namespace Wheelingful.UnitTests.Helpers;

public class TestDbContextFactory : IDbContextFactory<WheelingfulDbContext>
{
    public WheelingfulDbContext CreateDbContext()
    {
        var options = new DbContextOptionsBuilder<WheelingfulDbContext>()
            .UseInMemoryDatabase($"InMemoryTestDb-{DateTime.Now.ToFileTimeUtc()}")
            .Options;

        return new WheelingfulDbContext(options);
    }
}
