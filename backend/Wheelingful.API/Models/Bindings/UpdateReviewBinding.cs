using Wheelingful.API.Models.Bindings.Bodies;
using Wheelingful.BLL.Models.Requests;

namespace Wheelingful.API.Models.Bindings;

public class UpdateReviewBinding
{
    public int BookId { get; set; }
    public UpdateReviewBody Body { get; set; } = null!;

    public UpdateReviewRequest To()
    {
        return new UpdateReviewRequest
        {
            BookId = BookId,
            Title = Body.Title,
            Text = Body.Text,
            Score = Body.Score,
        };
    }
}
