using Wheelingful.BLL.Models.Requests;
using Wheelingful.BLL.Models.Responses;
using Wheelingful.BLL.Models.Responses.Generic;

namespace Wheelingful.BLL.Contracts.Books;

public interface IReviewService
{
    Task<FetchPaginationResponse<FetchReviewResponse>> GetReviews(FetchReviewPaginationRequest request);
    Task<FetchReviewResponse> GetReview(FetchReviewRequest request);
    Task CreateReview(CreateReviewRequest request);
    Task UpdateReview(UpdateReviewRequest request);
    Task DeleteReview(DeleteReviewRequest request);
}
