﻿namespace Wheelingful.BLL.Models.Requests;

public class UploadImageModel
{
    public required string Base64 { get; set; }
    public required string Name { get; set; }
    public required string Fodler { get; set; }
}