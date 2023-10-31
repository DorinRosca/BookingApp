using FluentValidation;

namespace Booking.Application.Features.Role.Commands.Update
{
    internal class UpdateRoleCommandValidator : AbstractValidator<UpdateRoleCommand>
    {
        public UpdateRoleCommandValidator()
        {
            RuleFor(x => x.RoleId)
                 .NotNull().WithMessage("The Role id is required")
                 .NotEmpty().WithMessage("The Role id cannot be empty");
            RuleFor(x => x.RoleName)
                 .NotNull().WithMessage("The Role name is required")
                 .NotEmpty().WithMessage("The Role name cannot be empty")
                 .MaximumLength(64).WithMessage("The Role name should be less than 64 characters long");
        }
    }
}
