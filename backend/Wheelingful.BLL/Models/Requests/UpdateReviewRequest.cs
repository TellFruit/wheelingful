﻿namespace Wheelingful.BLL.Models.Requests;

public class UpdateReviewRequest
{
    public int BookId { get; set; }
    public required string UserId { get; set; }
    public required string Title { get; set; }
    public required string Text { get; set; }
    public int Score { get; set; }
}
