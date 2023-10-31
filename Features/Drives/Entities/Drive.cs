using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using CarBookingApp.Features.Cars.Entities;

namespace CarBookingApp.Features.Drives.Entities
{
    [Table("Drive")]
    public class Drive
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public byte DriveId { get; set; }

        [Required, StringLength(50), Column(TypeName = "nvarchar(50)")]
        public string DriveName { get; set; }
        public virtual ICollection<Car> Cars { get; set; }

    }
}
