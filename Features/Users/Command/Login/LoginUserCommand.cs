using MediatR;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using CarBookingApp.Features.Users.ViewModel;

namespace CarBookingApp.Features.Users.Command.Login
{
    public record LoginUserCommand(LoginViewModel model) :IRequest<SignInResult>
     {
          [Required] [EmailAddress] 
          public string Email = model.Email;
          [Required]
          [DataType(DataType.Password)]

          public string Password = model.Password;

          [Display(Name = "Remember Me")] 
          public bool RememberMe = model.RememberMe;


     }

}
