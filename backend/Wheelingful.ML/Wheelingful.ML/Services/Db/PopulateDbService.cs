using Bogus;
using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using Wheelingful.ML.Constants;
using Wheelingful.ML.Models.Csv;
using Wheelingful.ML.Models.Db;

namespace Wheelingful.ML.Services.Db;

public class PopulateDbService
{
    private readonly IDictionary<string, int> _amazonBookIds;
    private readonly IDictionary<int, bool> _parsedBookIds;
    private readonly IDictionary<string, string> _amazonUserIds;
    private readonly IDictionary<string, bool> _parsedUserIds;
    private readonly Random _random;
    private readonly Faker _faker;

    public PopulateDbService()
    {
        _amazonBookIds = new Dictionary<string, int>();
        _parsedBookIds = new Dictionary<int, bool>();
        _amazonUserIds = new Dictionary<string, string>();
        _parsedUserIds = new Dictionary<string, bool>();
        _random = new Random();
        _faker = new Faker();
    }

    public void RunPopulate(string? dataSetName)
    {
        switch (dataSetName)
        {
            case "amazon":
                PopulateWithAmazonReviews();
                break;
            case "crossing":
                PopulateWithCrossingReviews();
                break;
            case "netflix":
                PopulateWithNetflixReviews();
                break;
            default:
                Console.WriteLine("There is no such dataset. Please, try again.");
                break;
        }
    }

    public void PopulateWithAmazonReviews()
    {
        using var streamReader = new StreamReader(CsvConstants.FileWithAmazonData);

        using var csvReader = new CsvReader(streamReader, CultureInfo.InvariantCulture);

        var records = csvReader.GetRecords<AmazonReviewRecord>();

        int countInvalid = 0;

        using var dbContext = new WheelingfulContext();

        foreach(var record in records)
        {
            if (!record.IsValidRecord()
                || IsDuplicateRecord(record.BookId, record.UserId))
            {
                countInvalid++;
                continue;
            }

            var now = DateTime.UtcNow;

            var email = _faker.Internet.Email(uniqueSuffix: Guid.NewGuid().ToString());
            var userId = ParseUserId(record.UserId, out bool isNewUser);
            var user = record.ToUser(userId, email, now);

            if (isNewUser)
            {
                dbContext.Add(user);
                dbContext.AddRange(new Aspnetuserrole
                {
                    UserId = userId,
                    RoleId = DbConstants.ReaderRoleId
                },
                new Aspnetuserrole
                {
                    UserId = userId,
                    RoleId = DbConstants.AuthorRoleId
                });
            }

            var bookId = ParseBookId(record.BookId, out bool isNewBook);
            var book = record.ToBook(bookId, userId, _random.Next(0, 2), now);

            if (isNewBook)
            {
                dbContext.Add(book);
            }

            var review = record.ToReview(bookId, userId, now);

            dbContext.Add(review);
        }

        dbContext.SaveChanges();
        
        Console.WriteLine("DONE!");
        Console.WriteLine(countInvalid + " Invalid");
    }

    public void PopulateWithCrossingReviews()
    {
        using var streamReader = new StreamReader(CsvConstants.FileWithCrossingData);

        using var csvReader = new CsvReader(streamReader, CultureInfo.InvariantCulture);

        var records = csvReader.GetRecords<CrossingReviewRecord>();

        int countInvalid = 0;

        using var dbContext = new WheelingfulContext();

        foreach (var record in records)
        {
            var now = DateTime.UtcNow;

            var email = _faker.Internet.Email(uniqueSuffix: Guid.NewGuid().ToString());
            var userId = ParseUserId(record.UserId, out bool isNewUser);
            var user = record.ToUser(userId, email, now);

            if (isNewUser)
            {
                dbContext.Add(user);
                dbContext.AddRange(new Aspnetuserrole
                {
                    UserId = userId,
                    RoleId = DbConstants.ReaderRoleId
                },
                new Aspnetuserrole
                {
                    UserId = userId,
                    RoleId = DbConstants.AuthorRoleId
                });
            }

            var bookId = ParseBookId(record.BookId, out bool isNewBook);
            var book = record.ToBook(bookId, userId, _random.Next(0, 2), now);

            if (isNewBook)
            {
                dbContext.Add(book);
            }

            var reviewTitle = _faker.Lorem.Sentence(5);
            var reviewText = _faker.Rant.Review("book");

            var review = record.ToReview(bookId, userId, reviewTitle, reviewText, now);

            dbContext.Add(review);
        }

        dbContext.SaveChanges();

        Console.WriteLine("DONE!");
        Console.WriteLine(countInvalid + " Invalid");
    }

    public void PopulateWithNetflixReviews()
    {
        using var streamReader = new StreamReader(CsvConstants.FileWithNetflixData);

        using var csvReader = new CsvReader(streamReader, CultureInfo.InvariantCulture);

        var records = csvReader.GetRecords<NetflixReviewRecord>();

        int countInvalid = 0;

        using var dbContext = new WheelingfulContext();

        foreach (var record in records)
        {
            var now = DateTime.UtcNow;

            var email = _faker.Internet.Email(uniqueSuffix: Guid.NewGuid().ToString());
            var userId = ParseUserId(record.UserId, out bool isNewUser);
            var user = record.ToUser(userId, email, now);

            if (isNewUser)
            {
                dbContext.Add(user);
                dbContext.AddRange(new Aspnetuserrole
                {
                    UserId = userId,
                    RoleId = DbConstants.ReaderRoleId
                },
                new Aspnetuserrole
                {
                    UserId = userId,
                    RoleId = DbConstants.AuthorRoleId
                });
            }

            var bookId = ParseBookId(record.BookId, out bool isNewBook);
            var bookTitle = _faker.Lorem.Sentence(4);
            var book = record.ToBook(bookId, userId, _random.Next(0, 2), now, bookTitle);

            if (isNewBook)
            {
                dbContext.Add(book);
            }

            var reviewText = _faker.Rant.Review("book");
            var reviewTitle = _faker.Lorem.Sentence(5);

            var review = record.ToReview(bookId, userId, now, reviewTitle, reviewText);

            dbContext.Add(review);
        }

        dbContext.SaveChanges();

        Console.WriteLine("DONE!");
        Console.WriteLine(countInvalid + " Invalid");
    }

    public void PopulateWitParsedReviews()
    {
        using var streamReader = new StreamReader(CsvConstants.FileWithFullParsedData);

        List<string> badRecords = new List<string>();
        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            Mode = CsvMode.RFC4180,
            Escape = '\\',
            BadDataFound = context => badRecords.Add(context.RawRecord)
        };

        using var csvReader = new CsvReader(streamReader, config);

        using var dbContext = new WheelingfulContext();

        var records = csvReader.GetRecords<ParsedReviewRecord>();
        
        foreach (var record in records)
        {
            var user = record.ToUser();

            if (IsNewParsedUser(user.Id))
            {
                dbContext.Add(user);
                dbContext.AddRange(new Aspnetuserrole
                {
                    UserId = user.Id,
                    RoleId = DbConstants.ReaderRoleId
                },
                new Aspnetuserrole
                {
                    UserId = user.Id,
                    RoleId = DbConstants.AuthorRoleId
                });
            }

            var book = record.ToBook();

            if (IsNewParsedBook(book.Id))
            {
                dbContext.Add(book);
            }

            var review = record.ToReview();

            dbContext.Add(review);
        }

        dbContext.SaveChanges();

        Console.WriteLine("DONE!");
    }

    public void ExportFullData()
    {
        var parsedDataFileName = CsvConstants.FileWithFullParsedData;

        using var dbContext = new WheelingfulContext();

        var mySqlSecurePath = GetSecureFilePriv(dbContext);

        var parsedDataFilePath = mySqlSecurePath + parsedDataFileName;

        dbContext.Database.ExecuteSql($@"
            SELECT 
                b.Id AS BookId,
                b.Title AS BookTitle,
                b.Status AS BookStatus,
                b.UserId AS BookAuthorId,
                u.Id AS UserId,
                u.Email AS UserEmail,
                r.Score AS ReviewScore,
                r.Title AS ReviewTitle,
                r.Text AS ReviewText,
                r.CreatedAt AS CreatedAt
            FROM reviews r
            INNER JOIN books b ON r.BookId = b.Id
            INNER JOIN aspnetusers u ON r.UserId = u.Id
            INTO OUTFILE {parsedDataFilePath}
            FIELDS TERMINATED BY ',' 
            OPTIONALLY ENCLOSED BY '""'
            LINES TERMINATED BY '\r\n';");

        string[] columnNames = {
        "BookId", "BookTitle", "BookStatus", "BookAuthorId", "UserId", "UserEmail",
        "ReviewScore", "ReviewTitle", "ReviewText", "CreatedAt"
        };

        MoveCsvExport(columnNames, parsedDataFileName, parsedDataFilePath);
    }

    public void TruncateAffectedTables()
    {
        using var dbContext = new WheelingfulContext();

        dbContext.Database.ExecuteSql($@"
            SET FOREIGN_KEY_CHECKS = 0;

            TRUNCATE TABLE wheelingful.aspnetusers;
            TRUNCATE TABLE wheelingful.aspnetuserroles;
            TRUNCATE TABLE wheelingful.books;
            TRUNCATE TABLE wheelingful.reviews;

            SET FOREIGN_KEY_CHECKS = 1;
            ");

        Console.WriteLine("DONE!");
    }

    private string GetSecureFilePriv(WheelingfulContext dbContext)
    {
        return dbContext.Database
            .SqlQuery<string>($"SELECT @@secure_file_priv;")
            .ToList()
            .First();
    }

    private void MoveCsvExport(string[] columnNames, string parsedDataFileName, string parsedDataFilePath)
    {
        var tempFilePath = Path.GetTempFileName();

        try
        {
            File.WriteAllText(tempFilePath, string.Join(",", columnNames) + Environment.NewLine);

            File.AppendAllText(tempFilePath, File.ReadAllText(parsedDataFilePath));

            File.Delete(parsedDataFilePath);

            File.Move(tempFilePath, parsedDataFileName, true);

            string fileContent = File.ReadAllText(parsedDataFileName);
        }
        finally
        {
            if (File.Exists(tempFilePath))
            {
                File.Delete(tempFilePath);
            }
        }

        Console.WriteLine("DONE!");
    }

    private static int LastIdentityBookId = 1;

    private int ParseBookId(string bookId, out bool isNewBook)
    {
        if (_amazonBookIds.TryGetValue(bookId, out int parsedBookId))
        {
            isNewBook = false;

            return parsedBookId;
        }

        isNewBook = true;

        parsedBookId = LastIdentityBookId++;

        _amazonBookIds.Add(bookId, parsedBookId);

        return parsedBookId;
    }

    private bool IsNewParsedBook(int bookId)
    {
        if (_parsedBookIds.ContainsKey(bookId))
        {
            return false;
        }

        _parsedBookIds.Add(bookId, true);

        return true;
    }

    private string ParseUserId(string userId, out bool isNewUser)
    {
        if (_amazonUserIds.TryGetValue(userId, out var parsedUserId))
        {
            isNewUser = false;

            return parsedUserId;
        }

        isNewUser = true;

        parsedUserId = Guid.NewGuid().ToString();

        _amazonUserIds.Add(userId, parsedUserId);

        return parsedUserId;
    }

    private bool IsNewParsedUser(string userId)
    {
        if (_parsedUserIds.ContainsKey(userId))
        {
            return false;
        }

        _parsedUserIds.Add(userId, true);

        return true;
    }

    private bool IsDuplicateRecord(string bookId, string userId)
    {
        return _amazonBookIds.ContainsKey(bookId) && _amazonUserIds.ContainsKey(userId);
    }
}
