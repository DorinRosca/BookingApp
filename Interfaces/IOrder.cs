using Car_Booking_App.Entities;
using CarBookingApp.ViewModels;

namespace CarBookingApp.Interfaces
{
     public interface IOrder
     {
          public Task<bool> CreateOrder(OrderViewModel model);
          public Task<IEnumerable<OrderViewModel>> GetOrders();
          public Task<IEnumerable<OrderViewModel>> GetUserOrders(string userId);
          public Task<bool> ConfirmOrder(int id); 
          public Task<bool> CancelOrder(int id);


     }
}
