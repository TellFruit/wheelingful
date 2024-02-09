using Microsoft.AspNetCore.Mvc;
using Wheelingful.API.Models.Bindings.Bodies;
using Wheelingful.BLL.Models.Requests;

namespace Wheelingful.API.Models.Bindings;

public class UpdateChapterBinding
{
    public int BookId { get; set; }
    public int ChapterId { get; set; }
    public UpdateChapterBody Body { get; set; } = null!;

    public UpdateChapterRequest To()
    {
        return new UpdateChapterRequest
        {
            BookId = BookId,
            ChapterId = ChapterId,
            Title = Body.Title,
            Text = Body.Text,
        };
    }
}
