using CarBookingApp.Interfaces;
using CarBookingApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CarBookingApp.Controllers
{
     public class UserController : Controller
     {
          private readonly IUser _user;

          public UserController(IUser user)
          {
               this._user = user;
          }

          public IActionResult Register()
          {
               return View();
          }

          [HttpPost]
          public IActionResult Register(RegisterViewModel model)
          {
               if (ModelState.IsValid)
               {
                    var result = _user.Register(model).Result;
                    if (result.Succeeded)
                    {
                         return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                         foreach (var error in result.Errors)
                         {
                              ModelState.AddModelError("", error.Description);
                         }

                         ModelState.AddModelError(string.Empty, "Invalid Register Attempt");
                    }
               }

               return View(model);
          }

          public IActionResult Login()
          {
               return View();
          }

          [HttpPost]
          public IActionResult Login(LoginViewModel model)
          {
               if (ModelState.IsValid)
               {
                    var result = _user.Login(model).Result;
                    if (result.Succeeded)
                    {
                         return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                         ModelState.AddModelError(string.Empty, "Invalid Login Attempt");
                    }
               }

               return View(model);
          }
          public IActionResult Logout()
          {
               _user.Logout();

               return RedirectToAction("Login");
          }
          [Authorize(Policy = "Admin")]
          public IActionResult Index()
          {
               var userRole = _user.GetAllUserRoles().Result;
               return View(userRole);
          }
          public IActionResult AddRole()
          {
               return View();
          }
          [Authorize(Policy = "Admin")]
          [HttpPost]
          public IActionResult AddRole(UserRoleViewModel model)
          {
               if (ModelState.IsValid)
               {
                    var result = _user.SetRole(model).Result;
                    if (result)
                    {
                         return RedirectToAction("Success", "Home");
                    }

               }
               return View(model);
          }

          [Authorize(Policy = "Admin")]
          [HttpPost]
          public IActionResult DeleteRole(UserRoleViewModel model)
          {
               if (ModelState.IsValid)
               {
                    var result = _user.DeleteRole(model).Result;
                    if (result)
                    {
                         return RedirectToAction("Success", "Home");
                    }
               }

               return RedirectToAction("Error", "Home");
          }
     }
}
