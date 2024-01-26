﻿using Microsoft.EntityFrameworkCore;
using Wheelingful.BLL.Contracts.Generic;
using Wheelingful.BLL.Models.Requests.General;
using Wheelingful.DAL.DbContexts;

namespace Wheelingful.BLL.Services.Generic;

public class CountPaginationPages<T>(WheelingfulDbContext dbContext) : ICountPaginationPages<T> where T : class
{
    public async Task<int> CountByPageSize(CountPagesRequest request)
    {
        int totalCount = await dbContext.Set<T>().CountAsync();

        return (int)Math.Ceiling((double)totalCount / request.PageSize);
    }
}