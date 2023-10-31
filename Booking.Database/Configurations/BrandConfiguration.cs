using Booking.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Booking.Database.Configurations
{
     internal class BrandConfiguration :IEntityTypeConfiguration<Brand>
     {
          public void Configure(EntityTypeBuilder<Brand> builder)
          {
               builder.ToTable(nameof(Brand));
               builder.HasKey(b => b.BrandId);
               builder.Property(b => b.BrandId).ValueGeneratedOnAdd();
               builder.Property(b => b.BrandName).IsRequired().HasMaxLength(64).HasColumnType("varchar(64)");
          }
     }
}
