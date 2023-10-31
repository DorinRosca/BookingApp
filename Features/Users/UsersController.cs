using CarBookingApp.Features.Users.Command.DeleteUserRole;
using CarBookingApp.Features.Users.Command.Login;
using CarBookingApp.Features.Users.Command.Logout;
using CarBookingApp.Features.Users.Command.Register;
using CarBookingApp.Features.Users.Command.SetRole;
using CarBookingApp.Features.Users.Query.GetUserRole;
using CarBookingApp.Features.Users.ViewModel;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CarBookingApp.Features.Users
{
    public class UsersController : Controller
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SignUp(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = _mediator.Send(new RegisterUserCommand(model)).Result;
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
                var result = _mediator.Send(new LoginUserCommand(model)).Result;
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
            var result = _mediator.Send(new LogoutUserCommand()).Result;
            return RedirectToAction("Login");
        }
        [Authorize(Policy = "Admin")]
        public IActionResult Index()
        {
            var userRole = _mediator.Send(new GetUserRoleQuery()).Result.ToList();
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
                var result = _mediator.Send(new SetRoleCommand(model)).Result;
                if (result)
                {
                    return RedirectToAction("Success", "Home");
                }
                return RedirectToAction("Error", "Home", new { errorMessage = "Cannot create new User-Role Relation.Data you inserted is invalid" });


            }
            return View(model);
        }

        [Authorize(Policy = "Admin")]
        [HttpPost]
        public IActionResult DeleteRole(UserRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = _mediator.Send(new DeleteUserRoleCommand(model)).Result;
                if (result)
                {
                    return RedirectToAction("Success", "Home");
                }
            }

            return RedirectToAction("Error", "Home");
        }
    }
}
