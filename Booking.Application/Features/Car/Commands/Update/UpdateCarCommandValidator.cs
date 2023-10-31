using FluentValidation;

namespace Booking.Application.Features.Car.Commands.Update
{
     internal class UpdateCarCommandValidator :AbstractValidator<UpdateCarCommand>
     {
          public UpdateCarCommandValidator()
          {
               RuleFor(x => x.CarId)
                   .NotNull().WithMessage("The car id is required")
                   .NotEmpty().WithMessage("The car id cannot be empty");

               RuleFor(x => x.Year)
                   .NotNull().WithMessage("The year is required")
                   .NotEmpty().WithMessage("The year cannot be empty")
                   .GreaterThanOrEqualTo((short)1900).WithMessage("The year cannot be earlier than 1900")
                   .LessThanOrEqualTo((short)DateTime.Now.Year).WithMessage("The year cannot be greater than the current year");

               RuleFor(x => x.ModelName)
                   .NotNull().WithMessage("The model name is required")
                   .NotEmpty().WithMessage("The model name cannot be empty")
                   .MaximumLength(64).WithMessage("The model name cannot have more than 64 characters");

               RuleFor(x => x.Image)
                   .NotNull().WithMessage("The image is required")
                   .NotEmpty().WithMessage("The image cannot be empty");

               RuleFor(x => x.PricePerDay)
                   .GreaterThanOrEqualTo(0).WithMessage("The price cannot be less than 0");

               RuleFor(x => x.FuelTypeId)
                   .GreaterThanOrEqualTo((byte)1).WithMessage("The fuel type id cannot be less than 1")
                   .NotNull().WithMessage("The fuel type id is required");

               RuleFor(x => x.VehicleId)
                   .GreaterThanOrEqualTo((byte)1).WithMessage("The vehicle id cannot be less than 1")
                   .NotNull().WithMessage("The vehicle id is required");

               RuleFor(x => x.BrandId)
                   .GreaterThanOrEqualTo((byte)1).WithMessage("The brand id cannot be less than 1")
                   .NotNull().WithMessage("The brand id is required");

               RuleFor(x => x.TransmissionId)
                   .GreaterThanOrEqualTo((byte)1).WithMessage("The transmission id cannot be less than 1")
                   .NotNull().WithMessage("The transmission id is required");

               RuleFor(x => x.DriveId)
                   .GreaterThanOrEqualTo((byte)1).WithMessage("The drive id cannot be less than 1")
                   .NotNull().WithMessage("The drive id is required");
          }
     }
}
