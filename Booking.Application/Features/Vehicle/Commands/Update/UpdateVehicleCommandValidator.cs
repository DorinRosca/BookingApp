using FluentValidation;

namespace Booking.Application.Features.Vehicle.Commands.Update
{
     internal class UpdateVehicleCommandValidator :AbstractValidator<UpdateVehicleCommand>
     {
          public UpdateVehicleCommandValidator()
          {
               RuleFor(x => x.VehicleId)
                    .NotNull().WithMessage("The vehicle id is required")
                    .NotEmpty().WithMessage("The vehicle id cannot be empty");
               RuleFor(x => x.VehicleName)
                    .NotNull().WithMessage("The Vehicle name is required")
                    .NotEmpty().WithMessage("The Vehicle name cannot be empty")
                    .MaximumLength(64).WithMessage("The Vehicle name should be less than 64 characters long");
               RuleFor(x => x.SeatNumber)
                    .NotNull().WithMessage("The Seat Number is required")
                    .NotEmpty().WithMessage("The Seat Number cannot be empty")
                    .GreaterThan((byte)0).WithMessage("The seat Number cannot be negative");
          }
     }
}
