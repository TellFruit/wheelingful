using MediatR;
using Wheelingful.BLL.Models.Responses;

namespace Wheelingful.BLL.Models.Requests.Queries;

public class RecommendByBookQuery : IRequest<IEnumerable<FetchBookResponse>>
{
    public int BookId { get; set; }
}
