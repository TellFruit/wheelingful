using Wheelingful.API.Models.Bindings.Bodies;
using Wheelingful.BLL.Models.Requests;
using Wheelingful.BLL.Models.Requests.Commands;

namespace Wheelingful.API.Models.Bindings;

public class CreateReviewBinding
{
    public int BookId { get; set; }
    public CreateReviewBody Body { get; set; } = null!;

    public CreateReviewCommand To()
    {
        return new CreateReviewCommand
        {
            BookId = BookId,
            Title = Body.Title,
            Text = Body.Text,
            Score = Body.Score,
        };
    }
}
