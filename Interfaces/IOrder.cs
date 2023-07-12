using Car_Booking_App.Entities;
using CarBookingApp.ViewModels;

namespace CarBookingApp.Interfaces
{
     public interface IOrder
     {
          public Task<bool> CreateOrder(OrderViewModel model);
     }
}
