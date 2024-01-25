using Microsoft.Extensions.Options;
using Wheelingful.BLL.Contracts.Books;
using Wheelingful.BLL.Contracts.Images;
using Wheelingful.BLL.Models.Books;
using Wheelingful.BLL.Models.Images;

namespace Wheelingful.BLL.Services.Books;

internal class BookCoverManager(IImageManager imageManager, IOptions<BookCoverOptions> options) : IBookCoverManager
{
    private readonly BookCoverOptions _coverOptions = options.Value;

    public string GetCoverUrl(string bookTitle, string authorId)
    {
        var coverName = GetBookCoverName(bookTitle, authorId);

        var relativePath = $"{_coverOptions.Folder}/{coverName}.{_coverOptions.Extension}";

        return imageManager.GetImageUrl(relativePath, new TransformationOptions
        {
            Width = _coverOptions.Width,
            Height = _coverOptions.Height
        });
    }

    public async Task<string> UploadCover(string base64, string bookTitle, string authorId)
    {
        return await imageManager.UploadImage(new UploadImageModel
        {
            Base64 = base64,
            Name = GetBookCoverName(bookTitle, authorId),
            Fodler = _coverOptions.Folder
        });
    }

    public async Task<string> UpdateCover(string imageId, string base64, string bookTitle, string authorId)
    {
        return await imageManager.UpdateImage(imageId, new UploadImageModel
        {
            Base64 = base64,
            Name = GetBookCoverName(bookTitle, authorId),
            Fodler = _coverOptions.Folder
        });
    }

    public async Task DeleteCover(string imageId)
    {
        await imageManager.DeleteImage(imageId);
    }

    private string GetBookCoverName(string bookTitle, string authorId)
    {
        var bookTitleRefined = string.Join("-", bookTitle.Split(" "));

        return $"{bookTitleRefined}_{authorId}";
    }
}
