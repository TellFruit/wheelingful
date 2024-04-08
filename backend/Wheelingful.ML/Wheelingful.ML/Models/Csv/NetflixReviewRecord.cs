using CsvHelper.Configuration.Attributes;
using Wheelingful.ML.Models.Db;

namespace Wheelingful.ML.Models.Csv;

public class NetflixReviewRecord
{
    [Name("bookId")]
    public required string BookId { get; set; }

    [Name("userId")]
    public required string UserId { get; set; }

    [Name("rating")]
    public double ReviewScore { get; set; }

    public Book ToBook(int bookId, string userId, int status, DateTime date, string bookTitle)
    {
        var truncatedTitle = bookTitle.Length > 255 ? bookTitle[..255] : bookTitle;

        return new Book
        {
            Id = bookId,
            Title = truncatedTitle,
            Description = truncatedTitle,
            Category = 0,
            Status = status,
            CreatedAt = date,
            UpdatedAt = date,
            UserId = userId,
            CoverId = string.Empty,
        };
    }

    public Aspnetuser ToUser(string id, string email, DateTime date)
    {
        return new Aspnetuser
        {
            Id = id,
            AccessFailedCount = 0,
            ConcurrencyStamp = Guid.NewGuid().ToString(),
            Email = email,
            EmailConfirmed = false,
            LockoutEnabled = false,
            LockoutEnd = null,
            NormalizedEmail = email.ToUpper(),
            NormalizedUserName = email.ToUpper(),
            PasswordHash = "AQAAAAIAAYagAAAAECgHupnB8wkJMaI20CCVFYYCvCAFFDsvb0eDQR0+GuK3KjAFYTbRynrrGFk/3NVwIA==",
            PhoneNumber = null,
            PhoneNumberConfirmed = false,
            SecurityStamp = Guid.NewGuid().ToString(),
            TwoFactorEnabled = false,
            UserName = email,
            CreatedAt = date,
            UpdatedAt = date,
        };
    }

    public Review ToReview(int bookId, string userId, DateTime date, string reviewTitle, string reviewText)
    {
        var truncatedTitle = reviewTitle.Length > 255 ? reviewTitle[..255] : reviewTitle;
        var truncatedText = reviewText.Length > 1000 ? reviewText[..1000] : reviewText;

        return new Review
        {
            BookId = bookId,
            UserId = userId,
            Score = (int)ReviewScore,
            Title = truncatedTitle,
            Text = truncatedText,
            CreatedAt = date,
            UpdatedAt = date,
        };
    }
}
