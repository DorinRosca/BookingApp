using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Booking.Database.Configurations
{
     internal class OrderConfiguration :IEntityTypeConfiguration<Domain.Entities.Order>
     {
          public void Configure(EntityTypeBuilder<Domain.Entities.Order> builder)
          {
               builder.ToTable(nameof(Order));
               builder.HasKey(o => o.OrderId);
               builder.Property(o => o.OrderId).ValueGeneratedOnAdd();
               builder.Property(o => o.CarId).IsRequired();
               builder.Property(o => o.StatusId).IsRequired();
               builder.Property(o => o.RentalStartDate).IsRequired().HasColumnType("datetime");
               builder.Property(o => o.RentalEndDate).IsRequired().HasColumnType("datetime");
               builder.Property(o => o.TotalAmount).IsRequired().HasColumnType("decimal(8,2)");
               //userId
               //Foreign Keys
               builder.HasOne(o => o.Car).WithMany(c => c.Orders).HasForeignKey(c => c.CarId);
               builder.HasOne(o => o.Status).WithMany(s => s.Orders).HasForeignKey(s => s.StatusId);
               builder.HasOne(o => o.User).WithMany(s => s.Orders).HasForeignKey(s => s.UserId);
          }
     }
}
