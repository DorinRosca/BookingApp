using FluentValidation;

namespace Booking.Application.Features.Order.Commands.Cancel
{
    internal class CancelOrderCommandValidator : AbstractValidator<CancelOrderCommand>
    {
        public CancelOrderCommandValidator()
        {
            RuleFor(x => x.OrderId)
                 .NotNull().WithMessage("The order Id is required")
                 .NotEmpty().WithMessage("The order id cannot be empty");
            
        }
    }
}
