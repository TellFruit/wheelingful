using System.Linq.Expressions;
using Wheelingful.BLL.Models.Requests.General;

namespace Wheelingful.BLL.Contracts.Generic;

public interface ICountPaginationPages<T> where T : class
{
    Task<int> CountByPageSize(CountPagesRequest request, IEnumerable<Expression<Func<T, bool>>>? filters = null);
}
