using FluentValidation;

namespace Booking.Application.Features.User.Commands.SetRole
{
    internal class SetUserRoleCommandValidator : AbstractValidator<SetUserRoleCommand>
    {
        public SetUserRoleCommandValidator()
        {
            RuleFor(x => x.RoleName)
                 .NotNull().WithMessage("The Role name is required")
                 .NotEmpty().WithMessage("The Role name cannot be empty");
            RuleFor(x => x.UserName)
                 .NotNull().WithMessage("The User name is required")
                 .NotEmpty().WithMessage("The User name cannot be empty");
          }
    }
}
