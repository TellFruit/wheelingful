using CsvHelper.Configuration.Attributes;
using System;
using Wheelingful.ML.Models.Db;

namespace Wheelingful.ML.Models.Csv;

public class CrossingReviewRecord
{
    [Name("isbn")]
    public required string BookId { get; set; }
    [Name("book_title")]
    public required string BookTitle { get; set; }
    [Name("user_id")]
    public required string UserId { get; set; }

    public bool IsValidRecord()
    {
        return !string.IsNullOrEmpty(BookId)
            && !string.IsNullOrEmpty(UserId);
    }

    public Book ToBook(int bookId, string userId, int status, DateTime date)
    {
        var truncatedTitle = BookTitle.Length > 255 ? BookTitle[..255] : BookTitle;

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

    public Review ToReview(int bookId, string userId, string reviewTitle, string reviewText, DateTime date)
    {
        var truncatedTitle = reviewTitle.Length > 255 ? reviewTitle[..255] : reviewTitle;
        var truncatedText = reviewText.Length > 1000 ? reviewText[..1000] : reviewText;

        return new Review
        {
            BookId = bookId,
            UserId = userId,
            Score = GenerateRating(),
            Title = truncatedTitle,
            Text = truncatedText,
            CreatedAt = date,
            UpdatedAt = date,
        };
    }

    private int GenerateRating()
    {
        double[] probabilities = { 0.1, 0.1, 0.3, 0.3, 0.2 };

        double randomNumber = new Random().NextDouble();
        double cumulativeProbability = 0;

        for (int i = 0; i < probabilities.Length; i++)
        {
            cumulativeProbability += probabilities[i];

            if (randomNumber < cumulativeProbability)
            {
                return i + 1;
            }
        }

        return 5;
    }
}
