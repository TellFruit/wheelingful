﻿using System.Text.Json.Serialization;
using Wheelingful.DAL.Enums;

namespace Wheelingful.BLL.Models.Requests;

public class CreateBookRequest
{
    public required string Title { get; set; }
    public required string CoverBase64 { get; set; }
    public required string Description { get; set; }

    [JsonConverter(typeof(JsonStringEnumConverter<BookCategoryEnum>))]
    public BookCategoryEnum Category { get; set; }

    [JsonConverter(typeof(JsonStringEnumConverter<BookStatusEnum>))]
    public BookStatusEnum Status { get; set; }
}
