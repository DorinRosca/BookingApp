using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Booking.Database.Configurations
{
     internal class StatusConfiguration :IEntityTypeConfiguration<Domain.Entities.Status>
     {
          public void Configure(EntityTypeBuilder<Domain.Entities.Status> builder)
          {
               builder.ToTable(nameof(Status));
               builder.HasKey(s => s.StatusId);
               builder.Property(s => s.StatusId).ValueGeneratedOnAdd();
               builder.Property(s => s.StatusName).IsRequired().HasMaxLength(64).HasColumnType("varchar(64)");
          }
     }
}
