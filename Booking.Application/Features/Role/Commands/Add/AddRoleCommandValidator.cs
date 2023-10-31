using FluentValidation;

namespace Booking.Application.Features.Role.Commands.Add
{
    internal class AddRoleCommandValidator : AbstractValidator<AddRoleCommand>
    {
        public AddRoleCommandValidator()
        {
            RuleFor(x => x.RoleName)
                 .NotNull().WithMessage("The Role name is required")
                 .NotEmpty().WithMessage("The Role name cannot be empty")
                 .MaximumLength(64).WithMessage("The Role name should be less than 64 characters long");
        }
    }
}
