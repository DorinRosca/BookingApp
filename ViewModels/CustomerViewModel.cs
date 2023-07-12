using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CarBookingApp.ViewModels
{
     public class CustomerViewModel
     {
          public int CustomerId { get; set; }

          [Required, StringLength(50)]
          public string FirstName { get; set; }

          [Required, StringLength(50)]
          public string LastName { get; set; }

          [Required, StringLength(50)]
          public string Email { get; set; }

          [Required, StringLength(50)]
          public string PhoneNumber { get; set; }
               
          [Required, StringLength(50)]
          public string Address { get; set; }

          [Required, StringLength(50)]
          public string City { get; set; }

          [Required, StringLength(50)]
          public string Country { get; set; }


          [Required, StringLength(20)]
          public string IDNP { get; set; }
          public int UserId { get; set; }
     }
}
