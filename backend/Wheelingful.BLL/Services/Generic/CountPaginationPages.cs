using Microsoft.EntityFrameworkCore;
using Wheelingful.BLL.Contracts.Generic;
using Wheelingful.BLL.Models.Requests.General;
using Wheelingful.DAL.DbContexts;
using System.Linq.Expressions;

namespace Wheelingful.BLL.Services.Generic;

public class CountPaginationPages<T>(WheelingfulDbContext dbContext) : ICountPaginationPages<T> where T : class
{
    public async Task<int> CountByPageSize(CountPagesRequest request, IEnumerable<Expression<Func<T, bool>>>? filters = null)
    {
        IQueryable<T> query = dbContext.Set<T>();

        if (filters != null)
        {
            foreach (var filterExpression in filters)
            {
                query = query.Where(filterExpression);
            }
        }

        int totalCount = await query.CountAsync();

        return (int)Math.Ceiling((double)totalCount / request.PageSize.Value);
    }
}
