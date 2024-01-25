using Wheelingful.Data.Enums;

namespace Wheelingful.Core.DTO.Books;

public class ReadBookModel
{
    public int Id { get; set; }
    public required string AuthorId { get; set; }
    public required string Title { get; set; }
    public required string Description { get; set; }
    public BookCategoryEnum Category { get; set; }
    public BookStatusEnum Status { get; set; }
    public required string CoverId { get; set; }
}
