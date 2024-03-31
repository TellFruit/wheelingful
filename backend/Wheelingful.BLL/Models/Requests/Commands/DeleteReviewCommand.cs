using MediatR;

namespace Wheelingful.BLL.Models.Requests.Commands;

public class DeleteReviewCommand : IRequest
{
    public int BookId { get; set; }
}
