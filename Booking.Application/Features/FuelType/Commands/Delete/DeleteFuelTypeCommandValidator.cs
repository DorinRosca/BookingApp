using FluentValidation;

namespace Booking.Application.Features.FuelType.Commands.Delete
{
    internal class DeleteFuelTypeCommandValidator : AbstractValidator<DeleteFuelTypeCommand>
    {
        public DeleteFuelTypeCommandValidator()
        {
            RuleFor(x => x.FuelTypeId)
                 .NotNull().WithMessage("The FuelType id is required")
                 .NotEmpty().WithMessage("The FuelType id cannot be empty");

        }
    }
}
