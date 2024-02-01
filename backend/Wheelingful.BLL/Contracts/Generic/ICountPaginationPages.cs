﻿using Wheelingful.BLL.Models.Requests.General;

namespace Wheelingful.BLL.Contracts.Generic;

public interface ICountPaginationPages<T> where T : class
{
    Task<int> CountByPageSize(CountPagesRequest request, Func<T, bool>? filter = null);
}
