using FluentValidation;
using Wheelingful.API.Extensions.Validation;
using Wheelingful.BLL.Models.Requests;
using Wheelingful.DAL.DbContexts;

namespace Wheelingful.API.Validators
{
    public class FetchBookValidator : AbstractValidator<FetchBookRequest>
    {
        public FetchBookValidator(WheelingfulDbContext dbContext)
        {
            RuleFor(b => b.BookId)
                .MustAsync((bookId, cancellation) => dbContext.Books.BeActualBook(bookId))
                    .WithMessage("There is no book with such ID");
        }
    }
}
