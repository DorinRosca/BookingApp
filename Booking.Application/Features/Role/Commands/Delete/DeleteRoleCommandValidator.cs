using FluentValidation;

namespace Booking.Application.Features.Role.Commands.Delete
{
    internal class DeleteRoleCommandValidator : AbstractValidator<DeleteRoleCommand>
    {
        public DeleteRoleCommandValidator()
        {
            RuleFor(x => x.RoleId)
                 .NotNull().WithMessage("The Role id is required")
                 .NotEmpty().WithMessage("The Role id cannot be empty");

        }
    }
}
