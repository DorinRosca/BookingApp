using FluentValidation;

namespace Booking.Application.Features.Status.Commands.Update
{
    internal class UpdateStatusCommandValidator : AbstractValidator<UpdateStatusCommand>
    {
        public UpdateStatusCommandValidator()
        {
            RuleFor(x => x.StatusId)
                 .NotNull().WithMessage("The Status id is required")
                 .NotEmpty().WithMessage("The Status id cannot be empty");
            RuleFor(x => x.StatusName)
                 .NotNull().WithMessage("The Status name is required")
                 .NotEmpty().WithMessage("The Status name cannot be empty")
                 .MaximumLength(64).WithMessage("The Status name should be less than 64 characters long");
        }
    }
}
