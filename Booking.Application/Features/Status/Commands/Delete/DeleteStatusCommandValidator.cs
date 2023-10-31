using FluentValidation;

namespace Booking.Application.Features.Status.Commands.Delete
{
    internal class DeleteStatusCommandValidator : AbstractValidator<DeleteStatusCommand>
    {
        public DeleteStatusCommandValidator()
        {
            RuleFor(x => x.StatusId)
                 .NotNull().WithMessage("The Status id is required")
                 .NotEmpty().WithMessage("The Status id cannot be empty");

        }
    }
}
