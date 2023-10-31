using Microsoft.AspNetCore.Identity;

namespace Booking.Domain.Entities
{
     public class ApplicationUser :IdentityUser
     {
          public virtual ICollection<Order>? Orders { get; set; }
     }
}
