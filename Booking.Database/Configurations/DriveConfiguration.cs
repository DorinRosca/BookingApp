using Booking.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Booking.Database.Configurations
{
     internal class DriveConfiguration :IEntityTypeConfiguration<Drive>
     {
          public void Configure(EntityTypeBuilder<Drive> builder)
          {
               builder.ToTable(nameof(Drive));
               builder.HasKey(d => d.DriveId);
               builder.Property(d => d.DriveId).ValueGeneratedOnAdd();
               builder.Property(d => d.DriveName).IsRequired().HasMaxLength(64).HasColumnType("varchar(64)");
          }
     }
}
