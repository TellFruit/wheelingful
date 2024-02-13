namespace Wheelingful.BLL.Models.Responses.Generic;

public class FetchPaginationResponse<TItems> where TItems : class
{
    public int PageCount { get; set; }
    public List<TItems> Items { get; set; } = [];
}
