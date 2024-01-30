using Wheelingful.BLL.Models.Requests;

namespace Wheelingful.BLL.Contracts.Chapters;

public interface IChapterAuthorService
{
    Task CreateChapter(CreateChapterRequest request);
    Task UpdateChapterProperties(UpdateChapterPropertiesRequest request);
    Task UpdateChapterText(UpdateChapterTextRequest request);
    Task DeleteChapter(DeleteChapterRequest request);
}
