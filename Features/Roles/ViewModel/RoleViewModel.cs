using System.ComponentModel.DataAnnotations;

namespace CarBookingApp.Features.Roles.ViewModel
{
    public class RoleViewModel
    {
        public string Id { get; set; }
        [StringLength(50)]

        public string NormalizedName { get; set; }
        [Required, StringLength(50)]
        public string Name { get; set; }
    }
}
