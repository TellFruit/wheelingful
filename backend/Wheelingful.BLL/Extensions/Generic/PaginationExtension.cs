using Microsoft.EntityFrameworkCore;
using Wheelingful.BLL.Models.Responses.Generic;

namespace Wheelingful.BLL.Extensions.Generic;

public static class PaginationExtension
{
    public static IQueryable<T> Paginate<T>(this IQueryable<T> values, int pageNumber, int pageSize)
    {
        var itemsToSkip = (pageNumber - 1) * pageSize;

        return values
            .Skip(itemsToSkip)
            .Take(pageSize);
    }

    public static async Task<int> CountPages<T>(this IQueryable<T> values, int pageSize)
    {
        var totalEntries = await values.CountAsync();

        return (int)Math.Ceiling((double)totalEntries / pageSize);
    }

    public static async Task<FetchPaginationResponse<T>> ToPagedListAsync<T>(this IQueryable<T> values, int pageNumber, int pageSize)
        where T : class
    {
        var pageCount = await values.CountPages(pageSize);

        var items = await values
            .Paginate(pageNumber, pageSize)
            .ToListAsync();

        return new FetchPaginationResponse<T>
        {
            PageCount = pageCount,
            Items = items
        };
    }
}
