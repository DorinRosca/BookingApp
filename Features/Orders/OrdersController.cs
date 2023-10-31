using CarBookingApp.Features.Cars.Query.GetById;
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
using QuestPDF.Helpers;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
using Unit = QuestPDF.Infrastructure.Unit;

namespace CarBookingApp.Features.Orders
{
    [Authorize]
    public class OrdersController : Controller
    {
        private readonly IMediator _mediator;
        private readonly UserManager<IdentityUser> _userManager;
        public OrdersController(IMediator mediator, UserManager<IdentityUser> userManager)
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
                if (order.RentalEndDate == order.RentalStartDate)
                {
                    return RedirectToAction("Error", "Home", new { errorMessage = "Order cannot have the same Start and End Date" });
                }
                order.UserId = _userManager.GetUserId(User) ?? throw new NullReferenceException();
                var result = _mediator.Send(new CreateOrderCommand(order)).Result;
                if (result != null)
                {
                     return RedirectToAction("GetReceipt", "Orders", result);

                }
                return RedirectToAction("Error", "Home", new { errorMessage = "The Car Is not Available" });
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
                return RedirectToAction("Login", "Users");
            var orders = _mediator.Send(new GetUserOrderQuery(userId)).Result;

            return View(orders);
        }
          public ActionResult GetReceipt(OrderViewModel order)
          {
               QuestPDF.Settings.License = LicenseType.Community;
               var data = Document.Create(container =>
               {
                    container.Page(page =>
                    {
                         page.Size(PageSizes.A4);
                         page.Margin(2, Unit.Centimetre);
                         page.PageColor(Colors.White);
                         page.DefaultTextStyle(x => x.FontSize(20));

                         page.Content()
                              .PaddingVertical(1, Unit.Centimetre)
                              .Column(x =>
                              {
                                   x.Spacing(20);

                                   // Order Id
                                   x.Item().Text($"Order ID: {order.OrderId}");

                                   // Car details (CarName and CarBrand)
                                   x.Item().Text($"Car Id: {order.CarId}");



                                   // User details (UserName)
                                   x.Item().Text($"User Id: {order.UserId}");

                                   // Rental StartDate
                                   x.Item().Text($"Rental Start Date: {order.RentalStartDate}");

                                   // Rental EndDate
                                   x.Item().Text($"Rental End Date: {order.RentalEndDate}");

                                   // Order Date
                                   x.Item().Text($"Order Date: {DateTime.Now}");
                                   // TotalAmount
                                   x.Item().Text($"Total Amount: {order.TotalAmount}");

                                   // Status

                              });
                    });
               })
                    .GeneratePdf();
               Stream stream = new MemoryStream(data);
               return File(stream, "application/pdf", "Rent Details.pdf");
          }
     }
}
