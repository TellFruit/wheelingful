using FluentValidation;
using Wheelingful.BLL.Models.Requests;

namespace Wheelingful.API.Validators;

public class CountBookPaginationPagesValidator : AbstractValidator<CountBookPaginationPagesRequest>
{
	public CountBookPaginationPagesValidator()
	{
		RuleFor(c => c.PageSize)
			.GreaterThan(0)
				.WithMessage("Page size must be greater than 0.")
            .LessThanOrEqualTo(100)
                .WithMessage("Page size must be less than 100.");
	}
}
