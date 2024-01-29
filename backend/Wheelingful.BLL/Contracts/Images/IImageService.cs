using Wheelingful.BLL.Models.Options;
using Wheelingful.BLL.Models.Requests;

namespace Wheelingful.BLL.Contracts.Images;

public interface IImageService
{
    Task<string> UploadImage(UploadImageRequest request);
    Task<string> UpdateImage(string imageId, UploadImageRequest request);
    Task DeleteImage(string imageId);
    string GetImageUrl(string imageId, TransformationOptions options);
}
