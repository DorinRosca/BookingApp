namespace Booking.Domain.Entities
{
    
     public class Status:AuditableEntity
     {
        public byte? StatusId { get; set; }
        public string? StatusName { get; set; }

        public virtual ICollection<Order>? Orders { get; set; }
     }
}
