using FluentValidation;

namespace Booking.Application.Features.FuelType.Commands.Update
{
    internal class UpdateFuelTypeCommandValidator : AbstractValidator<UpdateFuelTypeCommand>
    {
        public UpdateFuelTypeCommandValidator()
        {
            RuleFor(x => x.FuelTypeId)
                 .NotNull().WithMessage("The FuelType id is required")
                 .NotEmpty().WithMessage("The FuelType id cannot be empty");
            RuleFor(x => x.FuelTypeName)
                 .NotNull().WithMessage("The FuelType name is required")
                 .NotEmpty().WithMessage("The FuelType name cannot be empty")
                 .MaximumLength(64).WithMessage("The FuelType name should be less than 64 characters long");
        }
    }
}
