using FluentValidation;

namespace Booking.Application.Features.Drive.Commands.Delete
{
    internal class DeleteDriveCommandValidator : AbstractValidator<DeleteDriveCommand>
    {
        public DeleteDriveCommandValidator()
        {
            RuleFor(x => x.DriveId)
                 .NotNull().WithMessage("The Drive id is required")
                 .NotEmpty().WithMessage("The Drive id cannot be empty");

        }
    }
}
