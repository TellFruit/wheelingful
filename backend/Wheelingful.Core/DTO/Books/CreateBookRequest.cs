using Wheelingful.Core.DTO.Author.Abstract;

namespace Wheelingful.Core.DTO.Books;

public class CreateBookRequest : AuthorAuthorizeRequest
{
    public required NewBookModel NewBook { get; set; }
}
