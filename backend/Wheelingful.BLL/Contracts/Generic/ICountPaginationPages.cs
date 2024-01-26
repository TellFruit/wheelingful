namespace Wheelingful.BLL.Contracts.Generic;

public interface ICountPaginationPages<T> where T : class
{
    Task<int> CountByPageSize(int pageSize);
}
