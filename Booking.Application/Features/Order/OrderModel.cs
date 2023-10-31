using Booking.Application.Features.Car;
using Booking.Application.Features.Status;
using Booking.Application.Features.User;

namespace Booking.Application.Features.Order
{
    public class OrderModel
    {
        public int? OrderId { get; set; }
        public int? CarId { get; set; }
        public string? UserId { get; set; }
        public byte? StatusId { get; set; }

        public DateTime RentalStartDate { get; set; }
        public DateTime RentalEndDate { get; set; }
        public double? TotalAmount { get; set; }
        public CarModel? Car { get; set; }
        public UserModel? User { get; set; }
        public StatusModel? Status { get; set; }
    }
}
