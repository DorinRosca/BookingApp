using FluentValidation;

namespace Booking.Application.Features.Transmission.Commands.Delete
{
    internal class DeleteTransmissionRoleCommandValidator : AbstractValidator<DeleteTransmissionCommand>
    {
        public DeleteTransmissionRoleCommandValidator()
        {
            RuleFor(x => x.TransmissionId)
                 .NotNull().WithMessage("The Transmission id is required")
                 .NotEmpty().WithMessage("The Transmission id cannot be empty");

        }
    }
}
