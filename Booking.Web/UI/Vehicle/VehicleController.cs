using Booking.Application.Features.Vehicle;
using Booking.Application.Features.Vehicle.Commands.Add;
using Booking.Application.Features.Vehicle.Commands.Delete;
using Booking.Application.Features.Vehicle.Commands.Update;
using Booking.Application.Features.Vehicle.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Threading.Tasks;

namespace Booking.Web.UI.Vehicle
{
     [Authorize(Roles = "Admin")]
     public class VehicleController : Controller
     {
          private readonly IMediator _mediator;

          public VehicleController(IMediator mediator)
          {
               _mediator = mediator;
          }

          [HttpGet]
          public async Task<ActionResult> Index()
          {
               var getVehiclesQuery = new GetVehiclesQuery();
               var vehicles = await _mediator.Send(getVehiclesQuery);
               return View(vehicles);
          }

          [HttpGet]
          public ActionResult Create()
          {
               return View();
          }

          [HttpPost]
          [ValidateAntiForgeryToken]
          public async Task<ActionResult> Create(VehicleModel vehicleModel)
          {
               var addVehicleCommand = new AddVehicleCommand(vehicleModel);
               var insertedResult = await _mediator.Send(addVehicleCommand);
               return View(insertedResult);
          }

          [HttpPost]
          public async Task<ActionResult> Delete(byte vehicleId)
          {
               var deleteVehicleCommand = new DeleteVehicleCommand(vehicleId);
               var deleteResult = await _mediator.Send(deleteVehicleCommand);

               if (!deleteResult.DeleteIsSuccessful)
               {
                    return RedirectToAction("Error", "Home", deleteResult);
               }

               return RedirectToAction("Success", "Home");
          }

          public async Task<ActionResult> Update(byte vehicleId)
          {
               var getVehicleQuery = new GetVehicleQuery(vehicleId);
               var vehicleToUpdate = await _mediator.Send(getVehicleQuery);

               if (vehicleToUpdate != null)
               {
                    var data = new Tuple<UpdateVehicleResponse?, VehicleModel>(null, vehicleToUpdate);
                    return View(data);
               }

               return NotFound();
          }

          [HttpPost]
          [ValidateAntiForgeryToken]
          public async Task<IActionResult> Update(VehicleModel updatedVehicleModel)
          {
               var updateVehicleCommand = new UpdateVehicleCommand(updatedVehicleModel);
               var updateResult = await _mediator.Send(updateVehicleCommand);

               var data = new Tuple<UpdateVehicleResponse, VehicleModel?>(updateResult, updatedVehicleModel);
               return View(data);
          }
     }
}
