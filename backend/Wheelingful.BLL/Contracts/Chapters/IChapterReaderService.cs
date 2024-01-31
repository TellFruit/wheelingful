using Wheelingful.BLL.Models.Requests;
using Wheelingful.BLL.Models.Requests.General;
using Wheelingful.BLL.Models.Responses;

namespace Wheelingful.BLL.Contracts.Chapters;

public interface IChapterReaderService
{
    Task<List<FetchChapterPaginatedResponse>> GetChapters(FetchChapterPaginationRequest request);
    Task<int> CountPaginationPages(CountChapterPaginationPagesRequest request);
    Task<FetchChapterResponse> GetChapter(FetchChapterRequest request);
}
