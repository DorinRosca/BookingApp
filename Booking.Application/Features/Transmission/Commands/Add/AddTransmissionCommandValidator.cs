using FluentValidation;

namespace Booking.Application.Features.Transmission.Commands.Add
{
    internal class AddTransmissionCommandValidator : AbstractValidator<AddTransmissionCommand>
    {
        public AddTransmissionCommandValidator()
        {
            RuleFor(x => x.TransmissionName)
                 .NotNull().WithMessage("The Transmission name is required")
                 .NotEmpty().WithMessage("The Transmission name cannot be empty")
                 .MaximumLength(64).WithMessage("The Transmission name should be less than 64 characters long");
        }
    }
}
