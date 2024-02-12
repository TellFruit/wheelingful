using Wheelingful.API.Models.Bindings.Bodies;
using Wheelingful.BLL.Models.Requests;

namespace Wheelingful.API.Models.Bindings;

public class CreateChapterBinding
{
    public int BookId { get; set; }
    public CreateChapterBody Body { get; set; } = null!;

    public CreateChapterRequest To()
    {
        return new CreateChapterRequest
        {
            BookId = BookId,
            Title = Body.Title,
            Text = Body.Text,
        };
    }
}
