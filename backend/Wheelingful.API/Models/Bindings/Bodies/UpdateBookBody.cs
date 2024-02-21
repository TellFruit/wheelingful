using System.Text.Json.Serialization;
using Wheelingful.DAL.Enums;

namespace Wheelingful.API.Models.Bindings.Bodies;

public class UpdateBookBody
{
    public required string Title { get; set; }
    public string? CoverBase64 { get; set; }
    public required string Description { get; set; }

    [JsonConverter(typeof(JsonStringEnumConverter<BookCategoryEnum>))]
    public BookCategoryEnum Category { get; set; }

    [JsonConverter(typeof(JsonStringEnumConverter<BookStatusEnum>))]
    public BookStatusEnum Status { get; set; }
}
