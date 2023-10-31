using Booking.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Booking.Database.Configurations
{
     internal class FuelTypeConfiguration :IEntityTypeConfiguration<FuelType>
     {
          public void Configure(EntityTypeBuilder<FuelType> builder)
          {
               builder.ToTable(nameof(FuelType));
               builder.HasKey(f => f.FuelTypeId);
               builder.Property(f => f.FuelTypeId).ValueGeneratedOnAdd();
               builder.Property(f => f.FuelTypeName).IsRequired().HasMaxLength(64).HasColumnType("varchar(64)");
          }
     }
}
