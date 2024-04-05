using Microsoft.EntityFrameworkCore;
using Wheelingful.DAL.Entities;

namespace Wheelingful.API.Extensions.Validation;

public static class ValidateBookExtension
{
    public static Task<bool> BeActualBook(this DbSet<Book> set, int bookId)
    {
        return set.AnyAsync(b => b.Id == bookId);
    }

    public static Task<bool> BeActualBookAndAuthor(this DbSet<Book> set, int bookId, string userId)
    {
        return set.AnyAsync(b => b.Id == bookId && b.UserId == userId);
    }
}
