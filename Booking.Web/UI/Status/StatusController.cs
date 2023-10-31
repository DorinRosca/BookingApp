using Booking.Application.Features.Status;
using Booking.Application.Features.Status.Commands.Add;
using Booking.Application.Features.Status.Commands.Delete;
using Booking.Application.Features.Status.Commands.Update;
using Booking.Application.Features.Status.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Booking.Web.UI.Status
{
     [Authorize(Roles = "Admin")]
     public class StatusController : Controller
     {
          private readonly IMediator _mediator;

          public StatusController(IMediator mediator)
          {
               _mediator = mediator;
          }

          [HttpGet]
          public async Task<ActionResult> Index()
          {
               var getStatusesQuery = new GetStatusesQuery();
               var statuses = await _mediator.Send(getStatusesQuery);
               return View(statuses);
          }

          [HttpGet]
          public ActionResult Create()
          {
               return View();
          }

          [HttpPost]
          [ValidateAntiForgeryToken]
          public async Task<ActionResult> Create(StatusModel statusModel)
          {
               var addStatusCommand = new AddStatusCommand(statusModel);
               var insertedResult = await _mediator.Send(addStatusCommand);
               return View(insertedResult);
          }

          [HttpPost]
          [ValidateAntiForgeryToken]
          public async Task<ActionResult> Delete(byte id)
          {
               var deleteStatusCommand = new DeleteStatusCommand(id);
               var deleteResult = await _mediator.Send(deleteStatusCommand);

               if (!deleteResult.DeleteIsSuccessful)
               {
                    return RedirectToAction("Error", "Home", deleteResult);
               }

               return RedirectToAction("Success", "Home");
          }

          [HttpGet]
          public async Task<ActionResult> Update(byte id)
          {
               var getStatusQuery = new GetStatusQuery(id);
               var statusToUpdate = await _mediator.Send(getStatusQuery);

               if (statusToUpdate != null)
               {
                    var data = new Tuple<UpdateStatusResponse?, StatusModel>(null, statusToUpdate);
                    return View(data);
               }

               return NotFound();
          }

          [HttpPost]
          [ValidateAntiForgeryToken]
          public async Task<IActionResult> Update(StatusModel updatedStatusModel)
          {
               var updateStatusCommand = new UpdateStatusCommand(updatedStatusModel);
               var updateResult = await _mediator.Send(updateStatusCommand);

               var data = new Tuple<UpdateStatusResponse, StatusModel?>(updateResult, updatedStatusModel);
               return View(data);
          }
     }
}
