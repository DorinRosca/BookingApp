using System.Reflection;
using Booking.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Booking.Database
{
     public class BookingDbContext :IdentityDbContext
     {
          public  DbSet<Brand> Brand { get; set; }
          public  DbSet<Domain.Entities.Car> Car { get; set; }
          public  DbSet<Drive> Drive { get; set; }
          public  DbSet<FuelType> FuelType { get; set; }
          public  DbSet<Domain.Entities.Order> Order { get; set; }
          public  DbSet<Domain.Entities.Status> Status { get; set; }
          public  DbSet<Transmission> Transmission { get; set; }
          public  DbSet<Vehicle> Vehicle { get; set; }
          public  DbSet<ApplicationUser> User {get; set;}
          
          public BookingDbContext(DbContextOptions<BookingDbContext> options) :base(options) { }
          protected override void OnModelCreating(ModelBuilder modelBuilder)
          {
               base.OnModelCreating(modelBuilder);

               modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
          }
          public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
          {
               foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
               {
                    switch (entry.State)
                    {
                         case EntityState.Added:
                              entry.Entity.CreatedOn = DateTime.Now;
                              break;
                         case EntityState.Modified:
                              entry.Entity.ModifiedOn = DateTime.Now;
                              break;
                    }
               }
               return base.SaveChangesAsync(cancellationToken);
          }
     }
}
