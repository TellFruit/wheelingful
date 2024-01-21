using Wheelingful.Core.DTO.Author.Abstract;

namespace Wheelingful.Core.DTO.Books;

public class DeleteBookRequest : AuthorAuthorizeRequest
{
    public int BookId;
}
