using Booking.Application.Features.Drive;
using Booking.Application.Features.Drive.Commands.Add;
using Booking.Application.Features.Drive.Commands.Delete;
using Booking.Application.Features.Drive.Commands.Update;
using Booking.Application.Features.Drive.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Booking.Web.UI.Drive
{
     [Authorize(Roles = "Admin")]
     public class DriveController : Controller
     {
          private readonly IMediator _mediator;

          public DriveController(IMediator mediator)
          {
               _mediator = mediator;
          }

          [HttpGet]
          public async Task<ActionResult> Index()
          {
               var getDrivesQuery = new GetDrivesQuery();
               var drives = await _mediator.Send(getDrivesQuery);
               return View(drives);
          }

          [HttpGet]
          public ActionResult Create()
          {
               return View();
          }

          [HttpPost]
          [ValidateAntiForgeryToken]
          public async Task<ActionResult> Create(DriveModel driveModel)
          {
               var addDriveCommand = new AddDriveCommand(driveModel);
               var insertedResult = await _mediator.Send(addDriveCommand);
               return View(insertedResult);
          }

          [HttpGet]
          public async Task<ActionResult> Delete(byte id)
          {
               var deleteDriveCommand = new DeleteDriveCommand(id);
               var deleteResult = await _mediator.Send(deleteDriveCommand);

               if (!deleteResult.DeleteIsSuccessful)
               {
                    return RedirectToAction("Error", "Home", deleteResult);
               }

               return RedirectToAction("Success", "Home");
          }

          [HttpGet]
          public async Task<ActionResult> Update(byte id)
          {
               var getDriveQuery = new GetDriveQuery(id);
               var driveToUpdate = await _mediator.Send(getDriveQuery);

               if (driveToUpdate != null)
               {
                    var data = new Tuple<UpdateDriveResponse?, DriveModel>(null, driveToUpdate);
                    return View(data);
               }

               return NotFound();
          }

          [HttpPost]
          [ValidateAntiForgeryToken]
          public async Task<IActionResult> Update(DriveModel updatedDriveModel)
          {
               var updateDriveCommand = new UpdateDriveCommand(updatedDriveModel);
               var updateResult = await _mediator.Send(updateDriveCommand);

               var data = new Tuple<UpdateDriveResponse, DriveModel?>(updateResult, updatedDriveModel);
               return View(data);
          }
     }
}
