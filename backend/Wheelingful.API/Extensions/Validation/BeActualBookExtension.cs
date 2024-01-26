using Microsoft.EntityFrameworkCore;
using Wheelingful.DAL.Entities;

namespace Wheelingful.API.Extensions.Validation;

public static class BeActualBookExtension
{
    public static Task<bool> BeActualBook(this DbSet<Book> set, int bookId)
    {
        return set
            .Include(b => b.Users)
            .Select(b => new
            {
                b.Id,
                AuthorId = b.Users.First().Id,
            })
            .AnyAsync(b => b.Id == bookId);
    }
}
