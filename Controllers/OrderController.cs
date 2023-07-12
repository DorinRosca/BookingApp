using CarBookingApp.Interfaces;
using CarBookingApp.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
namespace CarBookingApp.Controllers
{
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
               var order = new OrderViewModel();
               order.CarId = carId;
               order.UserId = _userManager.GetUserId(User);
               return View(order); 
          }
     }
}
