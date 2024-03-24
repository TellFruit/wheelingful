using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Wheelingful.BLL.Contracts.Books;
using Wheelingful.BLL.Contracts.Images;
using Wheelingful.BLL.Models.Options;
using Wheelingful.BLL.Models.Requests;
using Wheelingful.DAL.DbContexts;

namespace Wheelingful.BLL.Services.Books;

internal class BookCoverService(
    IImageService imageManager, 
    ILogger<BookCoverService> logger, 
    IOptions<BookCoverOptions> options,
    WheelingfulDbContext dbContext) : IBookCoverService
{
    private readonly BookCoverOptions _coverOptions = options.Value;

    public string GetCoverUrl(int bookId, string authorId)
    {
        var coverId = dbContext.Books
            .Where(b => b.Id == bookId)
            .Select(b => b.CoverId)
            .First();

        var coverName = GetBookCoverName(bookId, authorId);

        string relativePath;
        if (string.IsNullOrEmpty(coverId))
        {
            relativePath = $"{_coverOptions.Folder}/{_coverOptions.DefaultCover}";
        }
        else
        {
            relativePath = $"{_coverOptions.Folder}/{coverName}";
        }

        return imageManager.GetImageUrl(relativePath, new TransformationOptions
        {
            Width = _coverOptions.Width,
            Height = _coverOptions.Height
        });
    }

    public Task<string> UploadCover(string base64, int bookId, string authorId)
    {
        logger.LogInformation("Uploading cover for book {BookId}", bookId);

        return imageManager.UploadImage(new UploadImageRequest
        {
            Base64 = base64,
            Name = GetBookCoverName(bookId, authorId),
            Fodler = _coverOptions.Folder
        });
    }

    public Task<string> UpdateCover(string imageId, string base64, int bookId, string authorId)
    {
        logger.LogInformation("Updating cover for book {BookId}", bookId);

        return imageManager.UpdateImage(imageId, new UploadImageRequest
        {
            Base64 = base64,
            Name = GetBookCoverName(bookId, authorId),
            Fodler = _coverOptions.Folder
        });
    }

    public Task DeleteCover(string imageId)
    {
        logger.LogInformation("Deleting cover {CoverId}", imageId);

        return imageManager.DeleteImage(imageId);
    }

    private string GetBookCoverName(int bookId, string authorId)
    {
        return $"{bookId}-{authorId}";
    }
}
