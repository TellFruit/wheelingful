using FluentValidation;
using Wheelingful.API.Extensions.Validation;
using Wheelingful.BLL.Models.Requests.Queries;
using Wheelingful.DAL.DbContexts;

namespace Wheelingful.API.Validators;

public class RecommendByBookValidator : AbstractValidator<RecommendByBookQuery>
{
    public RecommendByBookValidator(WheelingfulDbContext dbContext)
    {
        RuleFor(b => b.BookId)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
                .WithMessage("Book ID is required.")
            .MustAsync((id, cancelletion) => dbContext.Books.BeActualBook(id))
                .WithMessage("There is no book with such ID.");
    }
}
