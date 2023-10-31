using Booking.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Booking.Database.Configurations
{
     internal class TransmissionConfiguration:IEntityTypeConfiguration<Transmission>
     {
          public void Configure(EntityTypeBuilder<Transmission> builder)
          {
               builder.ToTable(nameof(Transmission));
               builder.HasKey(t => t.TransmissionId);
               builder.Property(t => t.TransmissionId).ValueGeneratedOnAdd();
               builder.Property(t => t.TransmissionName).IsRequired().HasMaxLength(64).HasColumnType("varchar(64)");
          }
     }
}
