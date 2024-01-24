using Wheelingful.Core.DTO.Images;

namespace Wheelingful.Core.Contracts.Images;

public interface IImageManager
{
    Task<string> UploadImage(UploadImageModel model);
    Task<string> UpdateImage(string imageId, UploadImageModel model);
    Task DeleteImage(string imageId);
    string GetImageUrl(string imageId, TransformationOptions options);
}
