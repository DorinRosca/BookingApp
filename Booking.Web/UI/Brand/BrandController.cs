using Booking.Application.Features.Brand;
using Booking.Application.Features.Brand.Commands.Add;
using Booking.Application.Features.Brand.Commands.Delete;
using Booking.Application.Features.Brand.Commands.Update;
using Booking.Application.Features.Brand.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Booking.Web.UI.Brand
{
     [Authorize(Roles = "Admin")]
     public class BrandController : Controller
     {
          private readonly IMediator _mediator;

          public BrandController(IMediator mediator)
          {
               _mediator = mediator;
          }

          [HttpGet]
          public async Task<ActionResult> Index()
          {
               var getBrandsQuery = new GetBrandsQuery();
               var brands = await _mediator.Send(getBrandsQuery);
               return View(brands);
          }

          [HttpGet]
          public ActionResult Create()
          {
               return View();
          }

          [HttpPost]
          [ValidateAntiForgeryToken]
          public async Task<ActionResult> Create(BrandModel brandModel)
          {
               var addBrandCommand = new AddBrandCommand(brandModel);
               var insertedResult = await _mediator.Send(addBrandCommand);
               return View(insertedResult);
          }

          [HttpPost]
          [ValidateAntiForgeryToken]
          public async Task<ActionResult> Delete(byte id)
          {
               var deleteBrandCommand = new DeleteBrandCommand(id);
               var deleteResult = await _mediator.Send(deleteBrandCommand);

               if (!deleteResult.DeleteIsSuccessful)
               {
                    return RedirectToAction("Error", "Home", deleteResult);
               }

               return RedirectToAction("Success", "Home");
          }

          [HttpGet]
          public async Task<ActionResult> Update(byte id)
          {
               var getBrandQuery = new GetBrandQuery(id);
               var brandToUpdate = await _mediator.Send(getBrandQuery);

               if (brandToUpdate != null)
               {
                    var data = new Tuple<UpdateBrandResponse?, BrandModel>(null, brandToUpdate);
                    return View(data);
               }

               return NotFound();
          }

          [HttpPost]
          [ValidateAntiForgeryToken]
          public async Task<IActionResult> Update(BrandModel updatedBrandModel)
          {
               var updateBrandCommand = new UpdateBrandCommand(updatedBrandModel);
               var updateResult = await _mediator.Send(updateBrandCommand);

               var data = new Tuple<UpdateBrandResponse, BrandModel?>(updateResult, updatedBrandModel);
               return View(data);
          }
     }
}
