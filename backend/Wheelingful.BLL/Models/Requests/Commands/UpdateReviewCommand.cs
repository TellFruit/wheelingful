using MediatR;

namespace Wheelingful.BLL.Models.Requests.Commands;

public class UpdateReviewCommand : IRequest
{
    public int BookId { get; set; }
    public required string Title { get; set; }
    public required string Text { get; set; }
    public int Score { get; set; }
}
