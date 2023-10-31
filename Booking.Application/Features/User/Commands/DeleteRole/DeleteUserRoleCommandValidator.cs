using FluentValidation;

namespace Booking.Application.Features.User.Commands.DeleteRole
{
    internal class DeleteUserRoleCommandValidator : AbstractValidator<DeleteUserRoleCommand>
    {
        public DeleteUserRoleCommandValidator()
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
