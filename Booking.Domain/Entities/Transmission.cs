namespace Booking.Domain.Entities
{

     public class Transmission :AuditableEntity
     {
        public byte? TransmissionId { get; set; }
        public string? TransmissionName { get; set; }

        public virtual ICollection<Car>? Cars { get; set; }
     }
}