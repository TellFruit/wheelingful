using Imagekit.Sdk;
using Wheelingful.BLL.Contracts.Images;
using Wheelingful.BLL.Models.Options;
using Wheelingful.BLL.Models.Requests;

namespace Wheelingful.BLL.Services.Images;

internal class ImageService(ImagekitClient imagekitClient) : IImageService
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

    public async Task<string> UploadImage(UploadImageRequest request)
    {
        var newImage = new FileCreateRequest
        {
            file = request.Base64,
            fileName = request.Name,
            folder = request.Fodler,
        };

        return (await imagekitClient.UploadAsync(newImage)).fileId;
    }

    public async Task<string> UpdateImage(string imageId, UploadImageRequest request)
    {
        await DeleteImage(imageId);
        return await UploadImage(request);
    }

    public async Task DeleteImage(string imageId)
    {
        await imagekitClient.DeleteFileAsync(imageId);
    }
}
