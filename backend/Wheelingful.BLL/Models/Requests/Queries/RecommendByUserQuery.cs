using MediatR;
using Wheelingful.BLL.Models.Responses;

namespace Wheelingful.BLL.Models.Requests.Queries;

public record RecommendByUserQuery() : IRequest<IEnumerable<FetchBookResponse>>;
