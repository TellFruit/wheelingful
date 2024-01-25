using Microsoft.Extensions.Options;
using Wheelingful.Core.Contracts.Books;
using Wheelingful.Core.Contracts.Images;
using Wheelingful.Core.DTO.Books;
using Wheelingful.Core.DTO.Images;

namespace Wheelingful.Core.Services.Books;

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
