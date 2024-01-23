using Wheelingful.Core.Enums;

namespace Wheelingful.Core.DTO.Books;

public class UpdatedBookModel
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public string? CoverBase64 { get; set; }
    public string Description { get; set; } = null!;
    public string? CoverId { get; set; }
    public BookCategoryEnum Category { get; set; }
    public BookStatusEnum Status { get; set; }
}
