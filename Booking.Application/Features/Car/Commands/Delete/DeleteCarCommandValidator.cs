using FluentValidation;

namespace Booking.Application.Features.Car.Commands.Delete
{
     internal class DeleteCarCommandValidator:AbstractValidator<DeleteCarCommand>
     {
          public DeleteCarCommandValidator()
          {
               RuleFor(x => x.CarId)
                    .NotNull().WithMessage("The car id is required")
                    .NotEmpty().WithMessage("The car id cannot be empty");

          }
     }
}
