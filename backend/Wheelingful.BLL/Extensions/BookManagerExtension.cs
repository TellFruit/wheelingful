using Microsoft.EntityFrameworkCore;
using Wheelingful.DAL.Entities;

namespace Wheelingful.BLL.Extensions;

public static class BookManagerExtension
{
    public static async Task<bool> IsActualAuthor(this DbSet<Book> set, int bookId, string userId)
    {
        return await set
            .Include(b => b.Users)
            .Select(b => new
            {
                b.Id,
                AuthorId = b.Users.First().Id,
            })
            .AnyAsync(b => b.Id == bookId && b.AuthorId == userId);
    }
}
