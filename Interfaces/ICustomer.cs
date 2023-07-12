using CarBookingApp.ViewModels;

namespace CarBookingApp.Interfaces
{
     public interface ICustomer
     {
          public Task<bool> AddCustomer(CustomerViewModel model);
          public Task<CustomerViewModel> GetCustomerByUser(int userId);    
     }
}
