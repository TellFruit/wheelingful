using FluentValidation;
using Wheelingful.API.Extensions.Validation;
using Wheelingful.BLL.Models.Requests;
using Wheelingful.DAL.DbContexts;

namespace Wheelingful.API.Validators;

public class FetchBookPaginationValidator : AbstractValidator<FetchBookPaginationRequest>
{
    public FetchBookPaginationValidator(WheelingfulDbContext dbContext)
    {
        RuleFor(b => b.AuthorId)
            .Cascade(CascadeMode.Stop)
            .MustAsync((id, cancellation) => dbContext.Users.BeActualUser(id!))
                .WithMessage("There is no author with such ID")
                .When(b => b.AuthorId != null);

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
