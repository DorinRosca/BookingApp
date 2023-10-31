namespace Booking.Domain.Entities
{
    
     public class Order :AuditableEntity
     {

        public int? OrderId { get; set; }
        public string? UserId { get; set; }
        public int? CarId { get; set; }
        public byte? StatusId { get; set; }
        public DateTime? RentalStartDate { get; set; }
        public DateTime? RentalEndDate { get; set; }
        public double? TotalAmount { get; set; }

        public ApplicationUser? User { get; set; }
        public Car? Car { get; set; }
        public Status? Status { get; set; }

     }
}
