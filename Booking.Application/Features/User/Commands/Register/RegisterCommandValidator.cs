using FluentValidation;

namespace Booking.Application.Features.User.Commands.Register;

public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
{
     public RegisterCommandValidator()
     {
          RuleFor(x => x.Email)
               .NotNull().WithMessage("The email is required")
               .NotEmpty().WithMessage("The email cannot be empty")
               .EmailAddress().WithMessage("Invalid email format"); 

          RuleFor(x => x.Password) 
               .NotNull().WithMessage("The password is required")
               .NotEmpty().WithMessage("The password cannot be empty")
               .MinimumLength(8).WithMessage("The password must be at least 8 characters long");

          RuleFor(x => x.ConfirmPassword)
               .NotNull().WithMessage("The confirm password is required")
               .NotEmpty().WithMessage("The confirm password cannot be empty")
               .Equal(x => x.Password).WithMessage("The confirm password does not match the password");
     }
}