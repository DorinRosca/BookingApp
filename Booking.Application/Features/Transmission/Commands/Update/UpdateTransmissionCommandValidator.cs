using FluentValidation;

namespace Booking.Application.Features.Transmission.Commands.Update
{
    internal class UpdateTransmissionCommandValidator : AbstractValidator<UpdateTransmissionCommand>
    {
        public UpdateTransmissionCommandValidator()
        {
            RuleFor(x => x.TransmissionId)
                 .NotNull().WithMessage("The Transmission id is required")
                 .NotEmpty().WithMessage("The Transmission id cannot be empty");
            RuleFor(x => x.TransmissionName)
                 .NotNull().WithMessage("The Transmission name is required")
                 .NotEmpty().WithMessage("The Transmission name cannot be empty")
                 .MaximumLength(64).WithMessage("The Transmission name should be less than 64 characters long");
        }
    }
}
