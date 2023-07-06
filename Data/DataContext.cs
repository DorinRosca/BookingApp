using Microsoft.EntityFrameworkCore;

namespace CarBookingApp.Data
{
     public class DataContext : DbContext
     {
          public DataContext(DbContextOptions options) : base(options)
          {
          }
     }
}
