using CarBookingApp.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace CarBookingApp.Interfaces
{
     public interface IUser
     {
          public Task<IdentityResult> Register(RegisterViewModel model);
          public Task<SignInResult> Login(LoginViewModel user);
          public void Logout();

          public Task<bool> SetRole(UserRoleViewModel model);
          public Task<bool> DeleteRole(UserRoleViewModel model);

          //public  Task<string> GetUserId(string name);
          //public  Task<string> GetRoleId(string name);

          public Task<List<UserRoleViewModel>> GetAllUserRoles();


     }
}
