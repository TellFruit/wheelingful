using System.Text.Json.Serialization;
using Wheelingful.DAL.Entities;
using Wheelingful.DAL.Enums;

namespace Wheelingful.BLL.Models.Responses;

public class FetchBookResponse
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public required string Description { get; set; }
    public string CoverUrl { get; set; } = string.Empty;
    public required string AuthorUserName { get; set; }
    public int AverageScore => Reviews.Any() ? (int)Reviews.Average(r => r.Score) : 0;

    [JsonConverter(typeof(JsonStringEnumConverter<BookCategoryEnum>))]
    public BookCategoryEnum Category { get; set; }

    [JsonConverter(typeof(JsonStringEnumConverter<BookStatusEnum>))]
    public BookStatusEnum Status { get; set; }

    public List<Review> Reviews { private get; set; } = new();
}
