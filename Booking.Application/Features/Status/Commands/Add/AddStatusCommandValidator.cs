using FluentValidation;

namespace Booking.Application.Features.Status.Commands.Add
{
    internal class AddStatusCommandValidator : AbstractValidator<AddStatusCommand>
    {
        public AddStatusCommandValidator()
        {
            RuleFor(x => x.StatusName)
                 .NotNull().WithMessage("The Status name is required")
                 .NotEmpty().WithMessage("The Status name cannot be empty")
                 .MaximumLength(64).WithMessage("The Status name should be less than 64 characters long");
        }
    }
}
