using Microsoft.Extensions.Options;
using Wheelingful.BLL.Contracts.Books;
using Wheelingful.BLL.Contracts.Images;
using Wheelingful.BLL.Models.Options;
using Wheelingful.BLL.Models.Requests;

namespace Wheelingful.BLL.Services.Books;

internal class BookCoverService(IImageService imageManager, IOptions<BookCoverOptions> options) : IBookCoverService
{
    private readonly BookCoverOptions _coverOptions = options.Value;

    public string GetCoverUrl(int bookId, string authorId)
    {
        var coverName = GetBookCoverName(bookId, authorId);

        var relativePath = $"{_coverOptions.Folder}/{coverName}";

        return imageManager.GetImageUrl(relativePath, new TransformationOptions
        {
            Width = _coverOptions.Width,
            Height = _coverOptions.Height
        });
    }

    public async Task<string> UploadCover(string base64, int bookId, string authorId)
    {
        return await imageManager.UploadImage(new UploadImageRequest
        {
            Base64 = base64,
            Name = GetBookCoverName(bookId, authorId),
            Fodler = _coverOptions.Folder
        });
    }

    public async Task<string> UpdateCover(string imageId, string base64, int bookId, string authorId)
    {
        return await imageManager.UpdateImage(imageId, new UploadImageRequest
        {
            Base64 = base64,
            Name = GetBookCoverName(bookId, authorId),
            Fodler = _coverOptions.Folder
        });
    }

    public async Task DeleteCover(string imageId)
    {
        await imageManager.DeleteImage(imageId);
    }

    private string GetBookCoverName(int bookId, string authorId)
    {
        return $"{bookId}-{authorId}";
    }
}
