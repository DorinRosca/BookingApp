using Booking.Application.Features.Order;
using Booking.Application.Features.Order.Commands.Add;
using Booking.Application.Features.Order.Commands.Cancel;
using Booking.Application.Features.Order.Commands.Confirm;
using Booking.Application.Features.Order.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using Unit = QuestPDF.Infrastructure.Unit;

namespace Booking.Web.UI.Order
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly IMediator _mediator;
        public OrderController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public IActionResult Create(int carId)
        {
             var order = new OrderModel
             {
                  CarId = carId
                     
             };
             var data = new Tuple<OrderModel, AddOrderResponse?>(order, null);
             return View(data);

        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(OrderModel order)
        {
             var cmd = new AddOrderCommand(order);
             var insertedResult = await _mediator.Send(cmd);
             var result = new Tuple<OrderModel, AddOrderResponse>(order, insertedResult);
             return View(result);
        }
        [Authorize]
        public async Task<IActionResult> Index()
        {
            if (!User.IsInRole("Admin"))
            {
                return RedirectToAction("List");
            }

            var query = new GetOrdersQuery();
            var data = await _mediator.Send(query);
            return View(data);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Confirm(int id)
        {
             var cmd = new ConfirmOrderCommand(id);
             var confirmResult = await _mediator.Send(cmd);
             return View(confirmResult);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Cancel(int id)
        {
             var cmd = new CancelOrderCommand(id);
             var confirmResult = await _mediator.Send(cmd);
             return View(confirmResult);
        }
          [Authorize]
        public async Task<IActionResult> List()
        {
             var query = new GetUserOrdersQuery();
            var result = await _mediator.Send(query);

            return View(result);
        }
          public ActionResult DownloadReceipt(OrderModel order)
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


                                   // Car details (CarName and CarBrand)
                                   x.Item().Text($"Car Id: {order.CarId}");



                                   // User details (UserName)
                                   x.Item().Text($"User Id: {order.UserId}");
                                   // Status
                                   x.Item().Text($"Status Id: {order.StatusId}");
                                   // Rental StartDate
                                   x.Item().Text($"Rental Start Date: {order.RentalStartDate}");

                                   // Rental EndDate
                                   x.Item().Text($"Rental End Date: {order.RentalEndDate}");

                                   // Order Date
                                   x.Item().Text($"Order Date: {DateTime.Now}");
                                   // TotalAmount
                                   x.Item().Text($"Total Amount: {order.TotalAmount}");


                              });
                    });
               })
                    .GeneratePdf();
               Stream stream = new MemoryStream(data);
               return File(stream, "application/pdf", "RentDetails.pdf");
          }
     }
}
