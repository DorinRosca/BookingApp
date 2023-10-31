namespace Booking.Domain.Entities
{
     public class FuelType :AuditableEntity
     {
        public byte? FuelTypeId { get; set; }
        public string? FuelTypeName { get; set; }

        public virtual ICollection<Car>? Cars { get; set; }
     }
}
