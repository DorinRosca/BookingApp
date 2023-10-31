using FluentValidation;

namespace Booking.Application.Features.Drive.Commands.Update
{
    internal class UpdateDriveCommandValidator : AbstractValidator<UpdateDriveCommand>
    {
        public UpdateDriveCommandValidator()
        {
            RuleFor(x => x.DriveId)
                 .NotNull().WithMessage("The Drive id is required")
                 .NotEmpty().WithMessage("The Drive id cannot be empty");
            RuleFor(x => x.DriveName)
                 .NotNull().WithMessage("The Drive name is required")
                 .NotEmpty().WithMessage("The Drive name cannot be empty")
                 .MaximumLength(64).WithMessage("The Drive name should be less than 64 characters long");
        }
    }
}
