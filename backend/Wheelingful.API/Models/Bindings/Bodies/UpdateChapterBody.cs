namespace Wheelingful.API.Models.Bindings.Bodies;

public class UpdateChapterBody
{
    public required string Title { get; set; }
    public string? Text { get; set; } = null!;
}
