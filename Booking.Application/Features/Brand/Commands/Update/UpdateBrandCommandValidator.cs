using FluentValidation;

namespace Booking.Application.Features.Brand.Commands.Update
{
    internal class UpdateBrandCommandValidator : AbstractValidator<UpdateBrandCommand>
    {
        public UpdateBrandCommandValidator()
        {
            RuleFor(x => x.BrandId)
                 .NotNull().WithMessage("The brand id is required")
                 .NotEmpty().WithMessage("The brand id cannot be empty");
            RuleFor(x => x.BrandName)
                 .NotNull().WithMessage("The brand name is required")
                 .NotEmpty().WithMessage("The brand name cannot be empty")
                 .MaximumLength(64).WithMessage("The brand name should be less than 64 characters long");
        }
    }
}
