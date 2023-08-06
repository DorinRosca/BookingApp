using CarBookingApp.Features.Brands.Entities;
using CarBookingApp.Features.Cars.Entities;
using CarBookingApp.Features.Drives.Entities;
using CarBookingApp.Features.FuelTypes.Entities;
using CarBookingApp.Features.Orders.Entities;
using CarBookingApp.Features.Statuses.Entities;
using CarBookingApp.Features.Transmissions.Entities;
using CarBookingApp.Features.Vehicles.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace CarBookingApp.Data
{
    public class DataContext : IdentityDbContext
     {
          public DataContext(DbContextOptions options) : base(options)
          {
          }
          public virtual DbSet<Brand> Brand { get; set; }
          public virtual DbSet<Car> Car { get; set; }
          public virtual DbSet<Drive> Drive { get; set; }
          public virtual DbSet<FuelType> FuelType { get; set; }
          public virtual DbSet<Order> Order { get; set; }
          public virtual DbSet<Status> Status { get; set; }
          public virtual DbSet<Transmission> Transmission { get; set; }
          public virtual DbSet<Vehicle> Vehicle { get; set; }
          protected override void OnModelCreating(ModelBuilder modelBuilder)
          {

               base.OnModelCreating(modelBuilder);
          }
     }
}
