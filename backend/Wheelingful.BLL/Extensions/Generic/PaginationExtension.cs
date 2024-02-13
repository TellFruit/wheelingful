using Microsoft.EntityFrameworkCore;

namespace Wheelingful.BLL.Extensions.Generic;

public static class PaginationExtension
{
    public static Task<List<T>> ToPagedListAsync<T>(this IQueryable<T> values, int pageNumber, int pageSize)
    {
        var itemsToSkip = (pageNumber - 1) * pageSize;

        return values
            .Skip(itemsToSkip)
            .Take(pageSize)
            .ToListAsync();
    }

    public static async Task<int> CountPages<T>(this IQueryable<T> values, int pageSize)
    {
        var totalEntries = await values.CountAsync();

        return (int)Math.Ceiling((double)totalEntries / pageSize);
    }
}
