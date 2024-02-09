using Wheelingful.DAL.Enums;

namespace Wheelingful.API.Models.Bindings.Bodies;

public class UpdateBookBody
{
    public required string Title { get; set; }
    public string? CoverBase64 { get; set; }
    public string Description { get; set; } = null!;
    public BookCategoryEnum Category { get; set; }
    public BookStatusEnum Status { get; set; }
}
