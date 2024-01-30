using Microsoft.Extensions.Options;
using Wheelingful.BLL.Contracts.Chapters;
using Wheelingful.BLL.Models.Options;

namespace Wheelingful.BLL.Services.Chapters;

public class ChapterTextService(IOptions<ChapterTextOptions> options) : IChapterTextService
{
    private readonly ChapterTextOptions _options = options.Value;

    public Task WriteText(string text, int chapterId, int bookId)
    {
        var fileDirectory = GetFileDirectory(bookId);

        if (!Directory.Exists(fileDirectory))
        {
            Directory.CreateDirectory(fileDirectory);
        }

        var relatedpath = GetFileRelatedPath(bookId, chapterId);

        return File.WriteAllTextAsync(relatedpath, text);
    }

    public void DeleteByChapter(int chapterId, int bookId)
    {
        var relatedpath = GetFileRelatedPath(bookId, chapterId);

        File.Delete(relatedpath);
    }

    public void DeleteByBook(int bookId)
    {
        var fileDirectory = GetFileDirectory(bookId); 

        if (Directory.Exists(fileDirectory))
        {
            Directory.Delete(fileDirectory, true);
        }
    }

    private string GetFileRelatedPath(int bookId, int chapterId)
    {
        return $"{GetFileDirectory(bookId)}\\{chapterId}.{_options.Extension}";
    }

    private string GetFileDirectory(int bookId)
    {
        return $"{_options.RootFolder}\\{bookId}";
    }
}
