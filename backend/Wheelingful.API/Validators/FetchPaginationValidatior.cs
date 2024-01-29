using FluentValidation;
using Wheelingful.BLL.Models.Requests.General;

namespace Wheelingful.API.Validators;

public class FetchPaginationValidatior : AbstractValidator<FetchPaginationRequest>
{
    public FetchPaginationValidatior()
    {
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
