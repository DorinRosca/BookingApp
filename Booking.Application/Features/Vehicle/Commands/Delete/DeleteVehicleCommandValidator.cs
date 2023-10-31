using FluentValidation;

namespace Booking.Application.Features.Vehicle.Commands.Delete
{
     internal class DeleteVehicleCommandValidator:AbstractValidator<DeleteVehicleCommand>
     {
          public DeleteVehicleCommandValidator()
          {
               RuleFor(x => x.VehicleId)
                    .NotNull().WithMessage("The Vehicle Id is required")
                    .NotEmpty().WithMessage("The Vehicle Id cannot be empty");

          }
     }
}
