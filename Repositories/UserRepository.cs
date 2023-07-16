using AutoMapper;
using Car_Booking_App.Entities;
using CarBookingApp.Data;
using CarBookingApp.Interfaces;
using CarBookingApp.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace CarBookingApp.Repositories
{
     public class UserRepository :IUser
     {
          private readonly DataContext _context;
          private readonly UserManager<IdentityUser> _userManager;
          private readonly SignInManager<IdentityUser> _signInManager;
          private readonly RoleManager<IdentityRole> _roleManager;

          private readonly IMapper _mapper;
          public UserRepository(DataContext context, IMapper mapper, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, RoleManager<IdentityRole> roleManager)
          {
               this._context = context;
               this._mapper = mapper;
               this._userManager = userManager;
               this._signInManager=signInManager;
               this._roleManager=roleManager;
          }
          public async Task<IdentityResult> Register(RegisterViewModel model)
          {
               if (model == null)
               {
                    throw new ArgumentNullException(nameof(model), "There is no such model");

               }

               var user = new IdentityUser
               {
                    UserName = model.Email,
                    Email = model.Email,
               };

               var result = await _userManager.CreateAsync(user, model.Password);
               if (result.Succeeded)
               {
                    await _signInManager.SignInAsync(user, isPersistent: false);

               }
               return result;
          }

          public async Task<SignInResult> Login(LoginViewModel user)
          {
                    var result = await _signInManager.PasswordSignInAsync(user.Email, user.Password, user.RememberMe, false);

                    return result;
          }

          public async void Logout()
          {
               await _signInManager.SignOutAsync();
          }

          public async Task<string> GetUserId(string name)
          {
               var result = await _userManager.FindByNameAsync(name);
               if (result is null)
               {
                    return null;
               }
               return result.Id;
          }
          public async Task<string> GetRoleId(string name)
          {
               var result = await _roleManager.FindByNameAsync(name);

               if (result is null)
                    return null;
               return result.Id;
          }


          public async Task<bool> SetRole(UserRoleViewModel model)
          {
               if (model == null)
               {
                    throw new ArgumentNullException(nameof(model), "There is no such model");

               }
               var roleId =  GetRoleId(model.RoleName).Result;
               var userId = GetUserId(model.UserName).Result;
               if (roleId != null && userId != null)
               {
                    var user = await _userManager.FindByIdAsync(userId);
                    var result = await _userManager.AddToRoleAsync(user, model.RoleName);
                    return result.Succeeded;
               }
               return false;
          }

          public async Task<bool> DeleteRole(UserRoleViewModel model)
          {
               if (model == null)
               {
                    throw new ArgumentNullException(nameof(model), "There is no such model");

               }
               var roleId = GetRoleId(model.RoleName).Result;
               var userId = GetUserId(model.UserName).Result;
               if (roleId != null && userId != null)
               {
                    var user = await _userManager.FindByIdAsync(userId);
                    var result = await _userManager.RemoveFromRoleAsync(user, model.RoleName);
                    return result.Succeeded;
               }

               return false;
          }
          public async Task<List<UserRoleViewModel>> GetAllUserRoles()
          {
               var users = await _userManager.Users.ToListAsync();
               var viewModels = new List<UserRoleViewModel>();

               foreach (var user in users)
               {
                    var roles = await _userManager.GetRolesAsync(user);

                    if (roles.Count == 0)
                    {
                         var viewModel = new UserRoleViewModel
                         {
                              UserName = user.UserName,
                         };

                         viewModels.Add(viewModel);
                    }
                    else
                    {
                         foreach (var role in roles)
                         {
                              var viewModel = new UserRoleViewModel
                              {
                                   UserName = user.UserName,
                                   RoleName = role
                              };

                              viewModels.Add(viewModel);
                         }
                    }
               }

               return viewModels;
          }
     }
}
