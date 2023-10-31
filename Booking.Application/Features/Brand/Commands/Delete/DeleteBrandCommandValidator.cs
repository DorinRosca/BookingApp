using FluentValidation;

namespace Booking.Application.Features.Brand.Commands.Delete
{
    internal class DeleteBrandCommandValidator : AbstractValidator<DeleteBrandCommand>
    {
        public DeleteBrandCommandValidator()
        {
            RuleFor(x => x.BrandId)
                 .NotNull().WithMessage("The brand id is required")
                 .NotEmpty().WithMessage("The brand id cannot be empty");

        }
    }
}
