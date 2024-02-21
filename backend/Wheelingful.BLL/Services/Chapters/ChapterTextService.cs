using Wheelingful.BLL.Constants;
using Wheelingful.BLL.Contracts.Chapters;

namespace Wheelingful.BLL.Services.Chapters;

public class ChapterTextService() : IChapterTextService
{
    public Task WriteText(string text, int chapterId, int bookId)
    {
        var fileDirectory = GetFileDirectory(bookId);

        if (!Directory.Exists(fileDirectory))
        {
            Directory.CreateDirectory(fileDirectory);
        }

        var path = GetFilePath(bookId, chapterId);

        return File.WriteAllTextAsync(path, text);
    }

    public Task<string> ReadText(int chapterId, int bookId)
    {
        var path = GetFilePath(bookId, chapterId);

        return File.ReadAllTextAsync(path);
    }

    public void DeleteByChapter(int chapterId, int bookId)
    {
        var path = GetFilePath(bookId, chapterId);

        File.Delete(path);
    }

    public void DeleteByBook(int bookId)
    {
        var fileDirectory = GetFileDirectory(bookId); 

        if (Directory.Exists(fileDirectory))
        {
            Directory.Delete(fileDirectory, true);
        }
    }

    private string GetFilePath(int bookId, int chapterId)
    {
        return Path.Combine(GetFileDirectory(bookId), $"{chapterId}.{FileConstants.ChapterFileExtension}");
    }
    
    private string GetFileDirectory(int bookId)
    {
        return Path.Combine(
            Environment.CurrentDirectory, 
            FileConstants.AppDataFolderName, 
            FileConstants.ChapterFolderName, 
            bookId.ToString());
    }
}
