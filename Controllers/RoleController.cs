using CarBookingApp.Interfaces;
using CarBookingApp.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CarBookingApp.Controllers
{
     public class RoleController : Controller
     {
          private readonly IRole _role;

          public RoleController(IRole role)
          {
               this._role = role;
          }
          public ActionResult Index()
          {
               var roles = _role.GetAllRoles();
               return View(roles.Result);
          }

          public ActionResult Create()
          {
               return View();
          }

          [HttpPost]
          [ValidateAntiForgeryToken]
          public ActionResult Create(RoleViewModel model)
          {
               var result = _role.AddRole(model).Result;
               if (result)
               {
                    return RedirectToAction("Success", "Home");
               }

               return RedirectToAction("Error", "Home");
          }

          [HttpPost]
          [ValidateAntiForgeryToken]
          public ActionResult Delete(string id)
          {
               var result = _role.DeleteRole(id).Result;
               if (result)
               {
                    return RedirectToAction("Success", "Home");
               }

               return RedirectToAction("Error", "Home");
          }

          public ActionResult Edit(string id)
          {
               RoleViewModel model = _role.GetRole(id).Result;
               if (model == null)
               {
                    return RedirectToAction("Error", "Home");
               }

               return View(model);
          }

          [HttpPost]
          [ValidateAntiForgeryToken]
          public async Task<IActionResult> Edit(RoleViewModel model)
          {
               if (!ModelState.IsValid)
               {
                    return View(model);
               }

               var result = await _role.EditRole(model);
               if (result)
               {
                    return RedirectToAction("Success", "Home");
               }

               return RedirectToAction("Error", "Home");
          }
     }
}
