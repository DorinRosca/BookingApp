using Booking.Application.Contracts.Database;
using Booking.Application.Contracts.Database.Car;
using Booking.Application.Contracts.Database.Order;
using Booking.Application.Contracts.Database.Status;
using Booking.Application.Contracts.Database.User;
using Booking.Database.Car;
using Booking.Database.Common;
using Booking.Database.Order;
using Booking.Database.Status;
using Booking.Database.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Booking.Database
{
     public static class DataBaseServiceRegistration
     {
          public static IServiceCollection AddDatabaseServices(this IServiceCollection services, ConfigurationManager config)
          {
               services.AddDbContextFactory<BookingDbContext>(options =>
               {
                    options.UseSqlServer(config.GetConnectionString("BookingDbConnection"));
               });
               //Common
               services.AddScoped(typeof(IAddEntity<>), typeof(AddEntity<>));
               services.AddScoped(typeof(IDeleteEntity<>), typeof(DeleteEntity<>));
               services.AddScoped(typeof(IGetEntityById<>), typeof(GetEntityById<>));
               services.AddScoped(typeof(ISelectAll<>), typeof(SelectAll<>));
               services.AddScoped(typeof(ISelectAllAsQueryable<>), typeof(SelectAllAsQueryable<>));
               services.AddScoped(typeof(IUpdateEntity<>), typeof(UpdateEntity<>));
               //Car
               services.AddScoped(typeof(IGetCarById), typeof(GetCarById));
               services.AddScoped(typeof(IGetCarsAsQueryable), typeof(GetCarsAsQueryable));
               services.AddScoped(typeof(ICheckCarAvailability), typeof(CheckCarAvailability));
               services.AddScoped(typeof(IGetCarPrice), typeof(GetCarPrice));
               //Order
               services.AddScoped(typeof(IGetAllOrders), typeof(GetAllOrders));
               services.AddScoped(typeof(IGetUserOrders), typeof(GetUserOrders));
               //User
               services.AddScoped(typeof(IGetUserByName), typeof(GetUserByName));
               services.AddScoped(typeof(IGetUserRoles), typeof(GetUserRoles));
               services.AddScoped(typeof(IGetUsers), typeof(GetUsers));
               services.AddScoped(typeof(ILogin), typeof(Login));
               services.AddScoped(typeof(ILogout), typeof(Logout));
               services.AddScoped(typeof(IRegister), typeof(Register));
               services.AddScoped(typeof(IRemoveUserRole), typeof(RemoveUserRole));
               services.AddScoped(typeof(ISetRole), typeof(SetRole));
               //Status
               services.AddScoped(typeof(IGetStatusIdByName), typeof(GetStatusIdByName));

               return services;
          }
     }
}
