using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using CarBookingApp.Features.Orders.Entities;

namespace CarBookingApp.Features.Statuses.Entities
{
    [Table("Status")]
    public class Status
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public byte StatusId { get; set; }

        [Required, StringLength(50), Column(TypeName = "nvarchar(50)")]
        public string StatusName { get; set; }

        public virtual ICollection<Order> Order { get; set; }

    }
}
