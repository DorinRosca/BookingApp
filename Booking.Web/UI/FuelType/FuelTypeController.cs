using Booking.Application.Features.FuelType;
using Booking.Application.Features.FuelType.Commands.Add;
using Booking.Application.Features.FuelType.Commands.Delete;
using Booking.Application.Features.FuelType.Commands.Update;
using Booking.Application.Features.FuelType.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Booking.Web.UI.FuelType
{
     [Authorize(Roles = "Admin")]
     public class FuelTypeController : Controller
     {
          private readonly IMediator _mediator;

          public FuelTypeController(IMediator mediator)
          {
               _mediator = mediator;
          }

          [HttpGet]
          public async Task<ActionResult> Index()
          {
               var getFuelTypesQuery = new GetFuelTypesQuery();
               var fuelTypes = await _mediator.Send(getFuelTypesQuery);
               return View(fuelTypes);
          }

          [HttpGet]
          public ActionResult Create()
          {
               return View();
          }

          [HttpPost]
          [ValidateAntiForgeryToken]
          public async Task<ActionResult> Create(FuelTypeModel fuelTypeModel)
          {
               var addFuelTypeCommand = new AddFuelTypeCommand(fuelTypeModel);
               var insertedResult = await _mediator.Send(addFuelTypeCommand);
               return View(insertedResult);
          }

          [HttpPost]
          [ValidateAntiForgeryToken]
          public async Task<ActionResult> Delete(byte id)
          {
               var deleteFuelTypeCommand = new DeleteFuelTypeCommand(id);
               var deleteResult = await _mediator.Send(deleteFuelTypeCommand);

               if (!deleteResult.DeleteIsSuccessful)
               {
                    return RedirectToAction("Error", "Home", deleteResult);
               }

               return RedirectToAction("Success", "Home");
          }

          [HttpGet]
          public async Task<ActionResult> Update(byte id)
          {
               var getFuelTypeQuery = new GetFuelTypeQuery(id);
               var fuelTypeToUpdate = await _mediator.Send(getFuelTypeQuery);

               if (fuelTypeToUpdate != null)
               {
                    var data = new Tuple<UpdateFuelTypeResponse?, FuelTypeModel>(null, fuelTypeToUpdate);
                    return View(data);
               }

               return NotFound();
          }

          [HttpPost]
          [ValidateAntiForgeryToken]
          public async Task<IActionResult> Update(FuelTypeModel updatedFuelTypeModel)
          {
               var updateFuelTypeCommand = new UpdateFuelTypeCommand(updatedFuelTypeModel);
               var updateResult = await _mediator.Send(updateFuelTypeCommand);

               var data = new Tuple<UpdateFuelTypeResponse, FuelTypeModel?>(updateResult, updatedFuelTypeModel);
               return View(data);
          }
     }
}
