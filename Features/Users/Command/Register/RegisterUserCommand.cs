using System.ComponentModel.DataAnnotations;
using CarBookingApp.Features.Users.ViewModel;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;

namespace CarBookingApp.Features.Users.Command.Register
{
    public record RegisterUserCommand : IRequest<IdentityResult>
    {
         [Required]
         [EmailAddress]
         public string Email { get; set; }
         [Required]
         [DataType(DataType.Password)]
         public string Password { get; set; }

         [DataType(DataType.Password)]
         [Display(Name = "Confirm Password")]
         [Compare("Password", ErrorMessage = "Password and confirmation password not match.")]
         public string ConfirmPassword { get; set; }

         public RegisterUserCommand(RegisterViewModel model)
         {
              Email = model.Email;
              Password = model.Password;
              ConfirmPassword = model.ConfirmPassword;
         }
     }
}
