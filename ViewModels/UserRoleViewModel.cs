using System.ComponentModel.DataAnnotations;

namespace CarBookingApp.ViewModels
{
     public class UserRoleViewModel
     {
          [Required, StringLength(50)]

          public string UserName { get; set; }
          [Required, StringLength(50)]
          public string RoleName { get; set; }
     }
}
