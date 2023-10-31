using FluentValidation;

namespace Booking.Application.Features.Drive.Commands.Add
{
    internal class AddDriveCommandValidator : AbstractValidator<AddDriveCommand>
    {
        public AddDriveCommandValidator()
        {
            RuleFor(x => x.DriveName)
                 .NotNull().WithMessage("The Drive name is required")
                 .NotEmpty().WithMessage("The Drive name cannot be empty")
                 .MaximumLength(64).WithMessage("The Drive name should be less than 64 characters long");
        }
    }
}
