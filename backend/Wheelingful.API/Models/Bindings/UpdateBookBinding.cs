using Microsoft.AspNetCore.Mvc;
using Wheelingful.API.Models.Bindings.Bodies;
using Wheelingful.BLL.Models.Requests;

namespace Wheelingful.API.Models.Bindings;

public class UpdateBookBinding
{
    public int BookId { get; set; }
    public UpdateBookBody Body { get; set; } = null!;

    public UpdateBookRequest To()
    {
        return new UpdateBookRequest
        {
            BookId = BookId,
            Title = Body.Title,
            CoverBase64 = Body.CoverBase64,
            Description = Body.Description,
            Category = Body.Category,
            Status = Body.Status,
        };
    }
}
