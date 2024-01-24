using Microsoft.EntityFrameworkCore;
using Wheelingful.Core.Contracts.Books;

namespace Wheelingful.Services.Extensions;

public static class BookManagerExtension
{
    public static async Task<bool> IsActualAuthor(this IBookManager manager, int bookId, string userId)
    {
        return await manager
            .GetBooks()
            .AnyAsync(b => b.Id == bookId && b.AuthorId == userId);
    }
}
