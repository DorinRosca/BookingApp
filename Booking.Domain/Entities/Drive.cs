namespace Booking.Domain.Entities
{ 
     public class Drive :AuditableEntity
     {
        public byte? DriveId { get; set; }
        public string? DriveName { get; set; }

        public virtual ICollection<Car>? Cars { get; set; }

    }
}
