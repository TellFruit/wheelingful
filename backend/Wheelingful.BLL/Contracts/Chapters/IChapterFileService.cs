namespace Wheelingful.BLL.Contracts.Chapters;

public interface IChapterTextService
{
    Task WriteText(string text, int chapterId, int bookId);
    Task<string> ReadText(int chapterId, int bookId);
    void DeleteByChapter(int chapterId, int bookId);
    void DeleteByBook(int bookId);
}
