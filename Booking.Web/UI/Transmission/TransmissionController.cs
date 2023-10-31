using Booking.Application.Features.Transmission;
using Booking.Application.Features.Transmission.Commands.Add;
using Booking.Application.Features.Transmission.Commands.Delete;
using Booking.Application.Features.Transmission.Commands.Update;
using Booking.Application.Features.Transmission.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Booking.Web.UI.Transmission
{
     [Authorize(Roles = "Admin")]
     public class TransmissionController : Controller
     {
          private readonly IMediator _mediator;

          public TransmissionController(IMediator mediator)
          {
               _mediator = mediator;
          }

          [HttpGet]
          public async Task<ActionResult> Index()
          {
               var getTransmissionsQuery = new GetTransmissionsQuery();
               var transmissions = await _mediator.Send(getTransmissionsQuery);
               return View(transmissions);
          }

          [HttpGet]
          public ActionResult Create()
          {
               return View();
          }

          [HttpPost]
          [ValidateAntiForgeryToken]
          public async Task<ActionResult> Create(TransmissionModel transmissionModel)
          {
               var addTransmissionCommand = new AddTransmissionCommand(transmissionModel);
               var insertedResult = await _mediator.Send(addTransmissionCommand);
               return View(insertedResult);
          }

          [HttpPost]
          [ValidateAntiForgeryToken]
          public async Task<ActionResult> Delete(byte id)
          {
               var deleteTransmissionCommand = new DeleteTransmissionCommand(id);
               var deleteResult = await _mediator.Send(deleteTransmissionCommand);

               if (!deleteResult.DeleteIsSuccessful)
               {
                    return RedirectToAction("Error", "Home", deleteResult);
               }

               return RedirectToAction("Success", "Home");
          }

          [HttpGet]
          public async Task<ActionResult> Update(byte id)
          {
               var getTransmissionQuery = new GetTransmissionQuery(id);
               var transmissionToUpdate = await _mediator.Send(getTransmissionQuery);

               if (transmissionToUpdate != null)
               {
                    var data = new Tuple<UpdateTransmissionResponse?, TransmissionModel>(null, transmissionToUpdate);
                    return View(data);
               }

               return NotFound();
          }

          [HttpPost]
          [ValidateAntiForgeryToken]
          public async Task<IActionResult> Update(TransmissionModel updatedTransmissionModel)
          {
               var updateTransmissionCommand = new UpdateTransmissionCommand(updatedTransmissionModel);
               var updateResult = await _mediator.Send(updateTransmissionCommand);

               var data = new Tuple<UpdateTransmissionResponse, TransmissionModel?>(updateResult, updatedTransmissionModel);
               return View(data);
          }
     }
}
