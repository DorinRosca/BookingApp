using System.ComponentModel.DataAnnotations;
using CarBookingApp.Features.Cars.ViewModel;
using MediatR;

namespace CarBookingApp.Features.Cars.Command.Create
{
    public record CreateCarCommand(CarCreateViewModel model) :IRequest<bool>
    {
         public int CarId = model.CarId;
         [Required]
         public byte VehicleId = model.VehicleId;

         [Required]
         public byte BrandId = model.BrandId;
         [Required]
         [StringLength(50)]
         public string ModelName = model.ModelName;
         [Required]
         [Range(1, short.MaxValue)]
         public short Year = model.Year;
         [Required]
         public byte FuelTypeId = model.FuelTypeId;

         [Required] public byte TransmissionId = model.TransmissionId;
         [Required]
         public byte DriveId = model.DriveId;

         [Required]
         [Range(1, double.PositiveInfinity)]
         public byte[] Image = model.Image;
         public decimal PricePerDay = model.PricePerDay;

         [Required]
         public IFormFile ImageFile = model.ImageFile;

     }
}
