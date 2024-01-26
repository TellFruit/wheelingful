using Wheelingful.DAL.Entities;
using Wheelingful.DAL.Enums;

namespace Wheelingful.BLL.Models.Responses;

public class FetchBookResponse
{
    public int Id { get; set; }
    public required string AuthorId { get; set; }
    public required string Title { get; set; }
    public required string Description { get; set; }
    public BookCategoryEnum Category { get; set; }
    public BookStatusEnum Status { get; set; }
    public required string CoverId { get; set; }
}
