using FluentValidation;

namespace Booking.Application.Features.Order.Commands.Confirm
{
    internal class ConfirmOrderCommandValidator : AbstractValidator<ConfirmOrderCommand>
    {
        public ConfirmOrderCommandValidator()
        {
            RuleFor(x => x.OrderId)
                 .NotNull().WithMessage("The order Id is required")
                 .NotEmpty().WithMessage("The order id cannot be empty");
            
        }
    }
}
