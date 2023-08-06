using CarBookingApp.Features.Orders.Command.Cancel;
using CarBookingApp.Features.Orders.Command.Confirm;
using CarBookingApp.Features.Orders.Command.Create;
using CarBookingApp.Features.Orders.Query.GetOrders;
using CarBookingApp.Features.Orders.Query.GetUserOrders;
using CarBookingApp.Features.Orders.ViewModel;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace CarBookingApp.Features.Orders
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly IMediator _mediator;
        private readonly UserManager<IdentityUser> _userManager;
        public OrderController(IMediator mediator, UserManager<IdentityUser> userManager)
        {
            _mediator = mediator;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create(int carId)
        {
            if (ModelState.IsValid)
            {

                var order = new OrderViewModel
                {
                     CarId = carId
                };
                return View(order);
            }
            return RedirectToAction("Error", "Home");

        }
        [Authorize]
        [HttpPost]
        public IActionResult Create(OrderViewModel order)
        {
            var itemsToRemove = new List<string> { "Car", "User", "Status", "UserId" };

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
                order.UserId = _userManager.GetUserId(User) ?? throw new NullReferenceException();
                var result = _mediator.Send(new CreateOrderCommand(order)).Result;
                if (result)
                {
                    return RedirectToAction("Success", "Home");
                }

                return RedirectToAction("Error", "Home", new { errorMessage = "The Car Is not Available" });
            }

            return View(order);
        }
        [Authorize]
        public IActionResult List()
        {
            //_order.UpdateStatuses();
            if (!User.IsInRole("Admin"))
            {
                return RedirectToAction("UserOrders");
            }

            var model = _mediator.Send(new GetOrdersQuery()).Result;
            return View(model);
        }
        [Authorize(Policy = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ConfirmOrder(int id)
        {
            if (ModelState.IsValid)
            {
                var result = _mediator.Send(new ConfirmOrderCommand(id)).Result;
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
                var result = _mediator.Send(new CancelOrderCommand(id)).Result;
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
            var orders = _mediator.Send(new GetUserOrderQuery(userId)).Result;

            return View(orders);
        }
    }
}
