namespace Wheelingful.Core.Contracts.Books;

public interface IBookCoverManager
{
    Task<string> UploadCover(string base64, string bookTitle, string authorId);
    Task<string> UpdateCover(string imageId, string base64, string bookTitle, string authorId);
    Task DeleteCover(string imageId);
    string GetCoverUrl(string bookTitle, string authorId);
}
