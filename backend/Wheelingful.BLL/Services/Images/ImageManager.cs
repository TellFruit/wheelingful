using Imagekit.Sdk;
using Wheelingful.BLL.Contracts.Images;
using Wheelingful.BLL.Models.Options;
using Wheelingful.BLL.Models.Requests;

namespace Wheelingful.BLL.Services.Images;

internal class ImageManager(ImagekitClient imagekitClient) : IImageManager
{
    public string GetImageUrl(string relativePath, TransformationOptions options)
    {
        var transform = new Transformation();

        if (options.Width != null)
        {
            transform.Width((int)options.Width);
        }

        if (options.Height != null)
        {
            transform.Height((int)options.Height);
        }

        return imagekitClient
            .Url(transform)
            .Path(relativePath)
            .TransformationPosition("query")
            .Generate();
    }

    public async Task<string> UploadImage(UploadImageModel model)
    {
        var newImage = new FileCreateRequest
        {
            file = model.Base64,
            fileName = model.Name,
            folder = model.Fodler,
        };

        return (await imagekitClient.UploadAsync(newImage)).fileId;
    }

    public async Task<string> UpdateImage(string imageId, UploadImageModel model)
    {
        await DeleteImage(imageId);
        return await UploadImage(model);
    }

    public async Task DeleteImage(string imageId)
    {
        await imagekitClient.DeleteFileAsync(imageId);
    }
}
