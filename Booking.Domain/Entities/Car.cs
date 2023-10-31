namespace Booking.Domain.Entities
{
    public class Car :AuditableEntity
    {
        public int? CarId { get; set; }

        public string? ModelName { get; set; }
        public short? Year { get; set; }
        public decimal? PricePerDay { get; set; }
        public byte[]? Image { get; set; }

        public byte? VehicleId { get; set; }
        public byte? BrandId { get; set; }
        public byte? FuelTypeId { get; set; }
        public byte? TransmissionId { get; set; }
        public byte? DriveId { get; set; }


        public Vehicle? Vehicle { get; set; }
        public Brand? Brand { get; set; }
        public FuelType? FuelType { get; set; }
        public Transmission? Transmission { get; set; }
        public Drive? Drive { get; set; }
        public ICollection<Order>? Orders { get; set; }




     }
}
