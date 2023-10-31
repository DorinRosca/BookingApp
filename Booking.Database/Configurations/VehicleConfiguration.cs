using Booking.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Booking.Database.Configurations
{
     internal class VehicleConfiguration :IEntityTypeConfiguration<Vehicle>
     {
          public void Configure(EntityTypeBuilder<Vehicle> builder)
          {
               builder.ToTable(nameof(Vehicle));
               builder.HasKey(v => v.VehicleId);
               builder.Property(v => v.VehicleId).ValueGeneratedOnAdd();
               builder.Property(v => v.VehicleName).IsRequired().HasMaxLength(64).HasColumnType("varchar(64)");
               builder.Property(v => v.SeatNumber).IsRequired().HasColumnType("tinyint");
          }
     }
}
