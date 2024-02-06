namespace Wheelingful.BLL.Contracts.Books;

public interface IBookCoverService
{
    Task<string> UploadCover(string base64, int bookId, string authorId);
    Task<string> UpdateCover(string imageId, string base64, int bookId, string authorId);
    Task DeleteCover(string imageId);
    string GetCoverUrl(int bookId, string authorId);
}
