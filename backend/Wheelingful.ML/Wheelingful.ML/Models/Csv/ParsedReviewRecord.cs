using CsvHelper.Configuration.Attributes;
using Wheelingful.ML.Models.Db;

namespace Wheelingful.ML.Models.Csv;

public class ParsedReviewRecord
{
    [Name("BookId")]
    public required int BookId { get; set; }
    [Name("BookTitle")]
    public required string BookTitle { get; set; }
    [Name("BookStatus")]
    public required int BookStatus { get; set; }
    [Name("BookAuthorId")]
    public required string BookAuthorId { get; set; }
    [Name("UserId")]
    public required string UserId { get; set; }
    [Name("UserEmail")]
    public required string UserEmail { get; set; }
    [Name("ReviewScore")]
    public double ReviewScore { get; set; }
    [Name("ReviewTitle")]
    public required string ReviewTitle { get; set; }
    [Name("ReviewText")]
    public required string ReviewText { get; set; }
    [Name("CreatedAt")]
    public DateTime CreatedAt { get; set; }

    public Book ToBook()
    {
        return new Book
        {
            Id = BookId,
            Title = BookTitle,
            Description = BookTitle,
            Category = 0,
            Status = BookStatus,
            CreatedAt = CreatedAt,
            UpdatedAt = CreatedAt,
            CoverId = string.Empty,
            UserId = UserId,
        };
    }

    public Aspnetuser ToUser()
    {
        return new Aspnetuser
        {
            Id = UserId,
            AccessFailedCount = 0,
            ConcurrencyStamp = Guid.NewGuid().ToString(),
            Email = UserEmail,
            EmailConfirmed = false,
            LockoutEnabled = false,
            LockoutEnd = null,
            NormalizedEmail = UserEmail.ToUpper(),
            NormalizedUserName = UserEmail.ToUpper(),
            PasswordHash = "AQAAAAIAAYagAAAAECgHupnB8wkJMaI20CCVFYYCvCAFFDsvb0eDQR0+GuK3KjAFYTbRynrrGFk/3NVwIA==",
            PhoneNumber = null,
            PhoneNumberConfirmed = false,
            SecurityStamp = Guid.NewGuid().ToString(),
            TwoFactorEnabled = false,
            UserName = UserEmail,
            CreatedAt = CreatedAt,
            UpdatedAt = CreatedAt,
        };
    }

    public Review ToReview()
    {
        return new Review
        {
            BookId = BookId,
            UserId = UserId,
            Score = (int)ReviewScore,
            Title = ReviewTitle,
            Text = ReviewText,
            CreatedAt = CreatedAt,
            UpdatedAt = CreatedAt,
        };
    }
}
