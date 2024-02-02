using FluentValidation;
using Wheelingful.API.Extensions.Validation;
using Wheelingful.BLL.Models.Requests;
using Wheelingful.DAL.DbContexts;

namespace Wheelingful.API.Validators;

public class FetchChapterPaginationValidator : AbstractValidator<FetchChapterPaginationRequest>
{
    public FetchChapterPaginationValidator(WheelingfulDbContext dbContext)
    {
        RuleFor(b => b.BookId)
            .MustAsync((bookId, cancellation) => dbContext.Books.BeActualBook(bookId))
                .WithMessage("There is no book with such ID");

        RuleFor(p => p.PageNumber)
            .GreaterThan(0)
                .WithMessage("Page number must be greater than 0.");

        RuleFor(p => p.PageSize)
            .GreaterThan(0)
                .WithMessage("Page size must be greater than 0.")
            .LessThanOrEqualTo(100)
                .WithMessage("Page size must be less than 100.");
    }
}
