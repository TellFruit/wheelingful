using Wheelingful.BLL.Models.Requests;
using Wheelingful.BLL.Models.Responses;
using Wheelingful.BLL.Models.Responses.Generic;

namespace Wheelingful.BLL.Contracts.Chapters;

public interface IChapterReaderService
{
    Task<FetchPaginationResponse<FetchChapterPropsResponse>> GetChapters(FetchChapterPaginationRequest request);
    Task<FetchChapterResponse> GetChapter(FetchChapterRequest request);
}
