using Wheelingful.API.Models.Bindings.Bodies;
using Wheelingful.BLL.Models.Requests;

namespace Wheelingful.API.Models.Bindings;

public class CreateReviewBinding
{
    public int BookId { get; set; }
    public CreateReviewBody Body { get; set; } = null!;

    public CreateReviewRequest To()
    {
        return new CreateReviewRequest
        {
            BookId = BookId,
            Title = Body.Title,
            Text = Body.Text,
            Score = Body.Score,
        };
    }
}
