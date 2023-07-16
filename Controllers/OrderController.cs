using Car_Booking_App.Entities;
using CarBookingApp.Interfaces;
using CarBookingApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace CarBookingApp.Controllers
{
     [Authorize]
     public class OrderController : Controller
     {
          private readonly IOrder _order;
          private readonly UserManager<IdentityUser> _userManager;
          public OrderController(IOrder order, UserManager<IdentityUser> userManager)
          {
               this._order = order;
               this._userManager = userManager;
          }
          public IActionResult Index()
          {
               return View();
          }

          public IActionResult Create(int carId)
          {
               if (ModelState.IsValid)
               {

                    var order = new OrderViewModel();
                    order.CarId = carId;
                    return View(order);
               }
               return RedirectToAction("Error", "Home");

          }
          [Authorize]
          [HttpPost]
          public IActionResult Create(OrderViewModel order)
          {
               var itemsToRemove = new List<string> { "Car", "User", "Status", "UserId"};

               foreach (var item in itemsToRemove)
               {
                    ModelState.Remove(item);
               }

               
               if (ModelState.IsValid)
               { 
                    if (order.RentalEndDate == order.RentalStartDate)
                    {
                         return RedirectToAction("Error", "Home", new { errorMessage = "Order cannot have the same Start and End Date" });
                    }
                    order.UserId = _userManager.GetUserId(User);

                    var result = _order.CreateOrder(order).Result;
                    if (result)
                    {
                         return RedirectToAction("Success", "Home");
                    }

                    return RedirectToAction("Error", "Home", new { errorMessage = "The Car Is not Available" });
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
               return View(order);
          }
          [Authorize]
          public IActionResult List()
          {
               if (!User.IsInRole("Admin"))
               {
                    return RedirectToAction("UserOrders");
               }

               var model = _order.GetOrders().Result;
               return View(model);
          }
          [Authorize(Policy = "Admin")]
          [HttpPost]
          [ValidateAntiForgeryToken]
          public IActionResult ConfirmOrder(int id)
          {
               if (ModelState.IsValid)
               {

                    var result = _order.ConfirmOrder(id).Result;
                    if (result)
                    {
                         return RedirectToAction("Success", "Home");
                    }
               }

               return RedirectToAction("Error", "Home");
          }
          [Authorize]
          [HttpPost]
          [ValidateAntiForgeryToken]
          public IActionResult CancelOrder(int id)
          {
               if (ModelState.IsValid)
               {
                    var result = _order.CancelOrder(id).Result;
                    if (result)
                    {
                         return RedirectToAction("Success", "Home");
                    }
               }

               return RedirectToAction("Error", "Home");
          }
          [Authorize]
          public IActionResult UserOrders()
          {
               var userId = _userManager.GetUserId(User);
               if (userId == null)
                    return RedirectToAction("Login", "User");
               var orders = _order.GetUserOrders(userId).Result;
               
               return View(orders);
          }
     }
}
