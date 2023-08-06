using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using CarBookingApp.Features.Cars.Entities;
namespace CarBookingApp.Features.Brands.Entities
{
    [Table("Brand")]
    public class Brand
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public byte BrandId { get; set; }

        [Required, StringLength(50), Column(TypeName = "nvarchar(50)")]
        public string BrandName { get; set; }

        public virtual ICollection<Car> Cars { get; set; }


    }
}
