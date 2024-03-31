using CsvHelper.Configuration.Attributes;
using Wheelingful.ML.Models.Db;

namespace Wheelingful.ML.Models.Csv;

public class AmazonReviewRecord
{
    [Name("Id")]
    public required string BookId { get; set; }
    [Name("Title")]
    public required string BookTitle { get; set; }
    [Name("User_id")]
    public required string UserId { get; set; }
    [Name("review/score")]
    public double ReviewScore { get; set; }
    [Name("review/summary")]
    public required string ReviewTitle { get; set; }
    [Name("review/text")]
    public required string ReviewText { get; set; }

    public bool IsValidRecord()
    {
        return !string.IsNullOrEmpty(BookId) 
            && !string.IsNullOrEmpty(UserId)
            && ReviewScore > 0 && ReviewScore <= 5;
    }

    public Book ToBook(int id, int status, DateTime date)
    {
        var truncatedTitle =  BookTitle.Length > 255 ? BookTitle[..255] : BookTitle;

        return new Book
        {
            Id = id,
            Title = truncatedTitle,
            Description = truncatedTitle,
            Category = 0,
            Status = status,
            CreatedAt = date,
            UpdatedAt = date,
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

    public Review ToReview(int bookId, string userId, DateTime date)
    {
        var truncatedTitle = ReviewTitle.Length > 255 ? ReviewTitle[..255] : ReviewTitle;
        var truncatedText = ReviewText.Length > 1000 ? ReviewText[..1000] : ReviewText;

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
