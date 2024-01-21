using Wheelingful.Core.DTO.Author.Abstract;

namespace Wheelingful.Core.DTO.Books;

public class UpdateBookRequest : AuthorAuthorizeRequest
{
    public required UpdatedBookModel UpdatedBook { get; set; }
}
