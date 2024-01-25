﻿using Imagekit.Sdk;
using Wheelingful.Core.Contracts.Images;
using Wheelingful.Core.DTO.Images;

namespace Wheelingful.Core.Services.Images;

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
