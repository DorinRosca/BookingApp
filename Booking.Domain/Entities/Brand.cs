namespace Booking.Domain.Entities
{
    public class Brand : AuditableEntity
    {
        public byte? BrandId { get; set; }
        public string? BrandName { get; set; }

        public virtual ICollection<Car>? Cars { get; set; }


    }
}
