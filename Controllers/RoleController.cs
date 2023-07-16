using CarBookingApp.Interfaces;
using CarBookingApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace CarBookingApp.Controllers
{
     public class RoleController : Controller
     {
          private readonly IRole _role;

          public RoleController(IRole role)
          {
               this._role = role;
          }
          [Authorize(Policy = "Admin")]
          public ActionResult Index()
          {
               var roles = _role.GetAllRoles();
               return View(roles.Result);
          }
          [Authorize(Policy = "Admin")]
          public ActionResult Create()
          {
               return View();
          }
          [Authorize(Policy = "Admin")]
          [HttpPost]
          [ValidateAntiForgeryToken]
          public ActionResult Create(RoleViewModel model)
          {
               var itemsToRemove = new List<string> { "NormalizedName", "Id" };

               foreach (var item in itemsToRemove)
               {
                    ModelState.Remove(item);
               }
               if (ModelState.IsValid)
               {
                    var result = _role.AddRole(model).Result;
                    if (result)
                    {
                         return RedirectToAction("Success", "Home");
                    }
               }
               var invalidFields = ModelState.Where(x => x.Value.ValidationState == ModelValidationState.Invalid)
                    .ToDictionary(x => x.Key, x => x.Value.Errors.Select(error => error.ErrorMessage).ToList());

               // Log or display the invalid fields and error messages
               foreach (var field in invalidFields)
               {
                    var fieldName = field.Key;
                    var errorMessages = field.Value;

                    // Log or display the fieldName and errorMessages as needed
                    // For example: Console.WriteLine($"Invalid field '{fieldName}': {string.Join(", ", errorMessages)}");
               }
               return RedirectToAction("Error", "Home");
               
          }
          [Authorize(Policy = "Admin")]
          [HttpPost]
          [ValidateAntiForgeryToken]
          public ActionResult Delete(string id)
          {
               var itemsToRemove = new List<string> { "NormalizedName", "Id" };

               foreach (var item in itemsToRemove)
               {
                    ModelState.Remove(item);
               }
               if (ModelState.IsValid)
               {
                    var result = _role.DeleteRole(id).Result;
                    if (result)
                    {
                         return RedirectToAction("Success", "Home");
                    }
               }
               return RedirectToAction("Error", "Home");
          }
          [Authorize(Policy = "Admin")]
          public ActionResult Edit(string id)
          {
               var itemsToRemove = new List<string> { "NormalizedName", "Id" };

               foreach (var item in itemsToRemove)
               {
                    ModelState.Remove(item);
               }
               if (ModelState.IsValid)
               {
                    RoleViewModel model = _role.GetRole(id).Result;
                    if (model == null)
                    {
                              return RedirectToAction("Error", "Home");
                    }  
                    return View(model);
  
               }
               var invalidFields = ModelState.Where(x => x.Value.ValidationState == ModelValidationState.Invalid)
                    .ToDictionary(x => x.Key, x => x.Value.Errors.Select(error => error.ErrorMessage).ToList());

               // Log or display the invalid fields and error messages
               foreach (var field in invalidFields)
               {
                    var fieldName = field.Key;
                    var errorMessages = field.Value;

                    // Log or display the fieldName and errorMessages as needed
                    // For example: Console.WriteLine($"Invalid field '{fieldName}': {string.Join(", ", errorMessages)}");
               }
               return RedirectToAction("Error", "Home");

          }
          [Authorize(Policy = "Admin")]
          [HttpPost]
          [ValidateAntiForgeryToken]
          public async Task<IActionResult> Edit(RoleViewModel model)
          {
               var itemsToRemove = new List<string> { "NormalizedName", "Id" };

               foreach (var item in itemsToRemove)
               {
                    ModelState.Remove(item);
               }
               if (ModelState.IsValid)
               {
                    var result = await _role.EditRole(model);
                    if (result)
                    {
                         return RedirectToAction("Success", "Home");
                    }
               }

               return View(model);
          }
     }
}
