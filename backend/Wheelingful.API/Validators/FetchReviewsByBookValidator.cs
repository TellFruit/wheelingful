using FluentValidation;
using Wheelingful.API.Extensions.Validation;
using Wheelingful.API.Models;
using Wheelingful.BLL.Contracts.Auth;
using Wheelingful.DAL.DbContexts;

namespace Wheelingful.API.Validators;

public class FetchReviewsByBookValidator : AbstractValidator<FetchReviewsByBookRequest>
{
    public FetchReviewsByBookValidator(WheelingfulDbContext dbContext)
    {
        RuleFor(b => b.BookId)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
                .WithMessage("Book ID is required.")
            .MustAsync((id, cancelletion) => dbContext.Books.BeActualBook(id))
                .WithMessage("There is no book with such ID.");

        RuleFor(b => b.PageNumber)
            .GreaterThan(0)
                .WithMessage("Page number must be greater than 0.");

        RuleFor(b => b.PageSize)
            .GreaterThan(0)
                .WithMessage("Page size must be greater than 0.")
            .LessThanOrEqualTo(100)
                .WithMessage("Page size must be less than 100.");
    }
}
