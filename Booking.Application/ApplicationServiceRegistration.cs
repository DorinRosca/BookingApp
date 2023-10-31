using Microsoft.Extensions.DependencyInjection;

namespace Booking.Application
{
     public static class ApplicationServiceRegistration
     {
          public static void AddApplicationServices(this IServiceCollection services)
          {
               services.AddMediatR(o =>
               {
                    o.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());
               });
               services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

          }
     }
}
