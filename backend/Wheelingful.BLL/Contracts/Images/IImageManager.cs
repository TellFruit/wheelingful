using Wheelingful.BLL.Models.Images;

namespace Wheelingful.BLL.Contracts.Images;

public interface IImageManager
{
    Task<string> UploadImage(UploadImageModel model);
    Task<string> UpdateImage(string imageId, UploadImageModel model);
    Task DeleteImage(string imageId);
    string GetImageUrl(string imageId, TransformationOptions options);
}
