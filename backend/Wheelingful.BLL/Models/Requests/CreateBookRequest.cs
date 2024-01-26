using Wheelingful.DAL.Enums;

namespace Wheelingful.BLL.Models.Requests;

public class CreateBookRequest
{
    public required string Title { get; set; }
    public required string CoverBase64 { get; set; }
    public string Description { get; set; } = null!;
    public BookCategoryEnum Category { get; set; }
    public BookStatusEnum Status { get; set; }
}
