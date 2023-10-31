using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Booking.Database.Configurations
{
     internal class CarConfiguration :IEntityTypeConfiguration<Domain.Entities.Car>
     {
          public void Configure(EntityTypeBuilder<Domain.Entities.Car> builder)
          {
               builder.ToTable(nameof(Car));
               builder.HasKey(c => c.CarId);
               builder.Property(c => c.CarId).ValueGeneratedOnAdd();
               builder.Property(c => c.ModelName).IsRequired().HasMaxLength(64).HasColumnType("varchar(64)");
               builder.Property(c => c.Year).IsRequired().HasColumnType("smallint");
               builder.Property(o => o.PricePerDay).IsRequired().HasColumnType("decimal(8,2)");
               builder.Property(o => o.Image).IsRequired().HasColumnType("varbinary(max)");

               builder.HasOne(c => c.Vehicle).WithMany(v => v.Cars).HasForeignKey(c => c.VehicleId);
               builder.HasOne(c => c.Brand).WithMany(v => v.Cars).HasForeignKey(c => c.BrandId);
               builder.HasOne(c => c.FuelType).WithMany(v => v.Cars).HasForeignKey(c => c.FuelTypeId);
               builder.HasOne(c => c.Transmission).WithMany(v => v.Cars).HasForeignKey(c => c.TransmissionId);
               builder.HasOne(c => c.Drive).WithMany(v => v.Cars).HasForeignKey(c => c.DriveId);
               builder.HasMany(c => c.Orders).WithOne(o => o.Car).HasForeignKey(c => c.CarId);
          }
     }
}
