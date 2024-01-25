using Microsoft.EntityFrameworkCore;

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
}
