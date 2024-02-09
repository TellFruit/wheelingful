using Wheelingful.BLL.Models.Requests;
using Wheelingful.BLL.Models.Requests.General;
using Wheelingful.BLL.Models.Responses;

namespace Wheelingful.BLL.Contracts.Chapters;

public interface IChapterReaderService
{
    Task<FetchChapterPaginationResponse> GetChapters(FetchChapterPaginationRequest request);
    Task<FetchChapterResponse> GetChapter(FetchChapterRequest request);
}
