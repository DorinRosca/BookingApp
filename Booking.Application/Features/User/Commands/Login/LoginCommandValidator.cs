using FluentValidation;

namespace Booking.Application.Features.User.Commands.Login
{
    internal class LoginCommandValidator : AbstractValidator<LoginCommand>
    {
        public LoginCommandValidator()
        {
             RuleFor(x => x.Email)
                  .NotNull().WithMessage("The email is required")
                  .NotEmpty().WithMessage("The email cannot be empty")
                  .EmailAddress().WithMessage("Invalid email format");

             RuleFor(x => x.Password)
                  .NotNull().WithMessage("The password is required")
                  .NotEmpty().WithMessage("The password cannot be empty")
                  .MinimumLength(8).WithMessage("The password must be at least 8 characters long");
          }
    }
}
