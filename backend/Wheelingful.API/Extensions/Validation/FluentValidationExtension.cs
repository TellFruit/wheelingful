using Microsoft.EntityFrameworkCore;
using Wheelingful.DAL.Entities;

namespace Wheelingful.API.Extensions.Validation;

public static class FluentValidationExtension
{
    public static bool BeValidBase64(this string value)
    {
        try
        {
            _ = Convert.FromBase64String(value);
            
            return true;
        }
        catch
        {
            return false;
        }
    }

    public static Task<bool> BeActualBookAndAuthor(this DbSet<Book> set, int bookId, string userId)
    {
        return set
            .Include(b => b.Users)
            .Select(b => new
            {
                b.Id,
                AuthorId = b.Users.First().Id,
            })
            .AnyAsync(b => b.Id == bookId && b.AuthorId == userId);
    }
}
