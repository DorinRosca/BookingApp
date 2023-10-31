using FluentValidation;

namespace Booking.Application.Features.FuelType.Commands.Add
{
    internal class AddFuelTypeCommandValidator : AbstractValidator<AddFuelTypeCommand>
    {
        public AddFuelTypeCommandValidator()
        {
            RuleFor(x => x.FuelTypeName)
                 .NotNull().WithMessage("The FuelType name is required")
                 .NotEmpty().WithMessage("The FuelType name cannot be empty")
                 .MaximumLength(64).WithMessage("The FuelType name should be less than 64 characters long");
        }
    }
}
