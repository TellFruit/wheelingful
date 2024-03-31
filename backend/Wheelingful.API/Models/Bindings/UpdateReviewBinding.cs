using Wheelingful.API.Models.Bindings.Bodies;
using Wheelingful.BLL.Models.Requests;
using Wheelingful.BLL.Models.Requests.Commands;

namespace Wheelingful.API.Models.Bindings;

public class UpdateReviewBinding
{
    public int BookId { get; set; }
    public UpdateReviewBody Body { get; set; } = null!;

    public UpdateReviewCommand To()
    {
        return new UpdateReviewCommand
        {
            BookId = BookId,
            Title = Body.Title,
            Text = Body.Text,
            Score = Body.Score,
        };
    }
}
