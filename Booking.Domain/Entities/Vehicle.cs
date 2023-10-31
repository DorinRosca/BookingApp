namespace Booking.Domain.Entities
{
 
     public class Vehicle:AuditableEntity    
     {
        public byte? VehicleId { get; set; }
        public string? VehicleName { get; set; }
        public byte? SeatNumber { get; set; }

        public virtual ICollection<Car>? Cars { get; set; }

     }
}
