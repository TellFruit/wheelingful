using FluentValidation;
using Wheelingful.BLL.Models.Requests.General;

namespace Wheelingful.API.Validators;

public class CountPagesValidator : AbstractValidator<CountPagesRequest>
{
	public CountPagesValidator()
	{
		RuleFor(c => c.PageSize)
			.GreaterThan(0)
				.WithMessage("Page size must be greater than 0.")
            .LessThanOrEqualTo(100)
                .WithMessage("Page size must be less than 100.");
	}
}
