     using Booking.Application.Features.Car;
     using Booking.Application.Features.Car.Commands.Add;
     using Booking.Application.Features.Car.Commands.Delete;
     using Booking.Application.Features.Car.Commands.Update;
     using Booking.Application.Features.Car.Queries;
     using MediatR;
     using Microsoft.AspNetCore.Mvc;
using System.Data;
     using Microsoft.AspNetCore.Authorization;

     namespace Booking.Web.UI.Car
     {
         public class CarController : Controller
         {
             private readonly IMediator _mediator;

             public CarController(IMediator mediator)
             {
                    _mediator = mediator;
             }
             public async Task<ActionResult> Index(CarFilterModel model)
             {
                  var query = new GetCarsQuery(model);
                  var cars = await _mediator.Send(query);
                  return View(cars);
             }

             [Authorize(Roles = "Admin")]
             public async Task<IActionResult> Create()
             {
                  var query = new GetCarDetailsQuery();
                  var result = await _mediator.Send(query);
                  var data = new Tuple<CarViewModel?, AddCarResponse?>(result, null);
                  return View(data);
             }

           [Authorize(Roles = "Admin")]
             [HttpPost]
             [ValidateAntiForgeryToken]
             public async Task<IActionResult> Create(CarModel model)
             {
                  var cmd = new AddCarCommand(model);
                  var insertedResult = await _mediator.Send(cmd);
                  var query = new GetCarDetailsQuery();
                  var details = await _mediator.Send(query);

                  var data = new Tuple<CarViewModel?, AddCarResponse?>(details,insertedResult);
                  return View(data);
             }

             public async Task<IActionResult> Details(int id)
             {
                  var query = new GetCarQuery(id);
                  var result = await _mediator.Send(query);
                  return View(result);
             }
               [Authorize(Roles = "Admin")]
               public async Task<IActionResult> Edit(int id)
               {
                  var query = new GetCarUpdateQuery(id);
                  var result = await _mediator.Send(query);
                  var data = new Tuple<CarViewModel?,UpdateCarResponse?>(result, null);
                  return View(data);

             }
             [Authorize(Roles = "Admin")]
             [HttpPost]
             [ValidateAntiForgeryToken]
             public async Task<IActionResult> Edit(CarViewModel? model)
             {
                  var cmd = new UpdateCarCommand(model);
                  var result = await _mediator.Send(cmd);
                  var query = new GetCarUpdateQuery(model?.CarId);
                  model = await _mediator.Send(query);
               var data = new Tuple<CarViewModel?, UpdateCarResponse?>(model, result);

               return View(data);

             }
             [Authorize(Roles = "Admin")]
             [HttpPost]
             public async Task<ActionResult> Delete(int id)
             {
                  var cmd = new DeleteCarCommand(id);
                  var result = await _mediator.Send(cmd);
                  return View(result);
             }
         }
     }
