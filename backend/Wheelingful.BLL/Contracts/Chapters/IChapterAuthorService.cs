using Wheelingful.BLL.Models.Requests;

namespace Wheelingful.BLL.Contracts.Chapters;

public interface IChapterAuthorService
{
    Task CreateChapter(CreateChapterRequest request);
    Task UpdateChapter(UpdateChapterRequest request);
    Task DeleteChapter(DeleteChapterRequest request);
}
