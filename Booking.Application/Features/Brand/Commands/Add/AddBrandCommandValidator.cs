using FluentValidation;

namespace Booking.Application.Features.Brand.Commands.Add
{
    internal class AddBrandCommandValidator : AbstractValidator<AddBrandCommand>
    {
        public AddBrandCommandValidator()
        {
            RuleFor(x => x.BrandName)
                 .NotNull().WithMessage("The brand name is required")
                 .NotEmpty().WithMessage("The brand name cannot be empty")
                 .MaximumLength(64).WithMessage("The brand name should be less than 64 characters long");
        }
    }
}
