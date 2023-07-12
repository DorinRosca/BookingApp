using CarBookingApp.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace CarBookingApp.Interfaces
{
     public interface IRole
     {
          public Task<IEnumerable<RoleViewModel>> GetAllRoles();

          public Task<bool> AddRole(RoleViewModel model);

          public Task<bool> EditRole(RoleViewModel model);

          public Task<bool> DeleteRole(string id);

          public Task<RoleViewModel> GetRole(string id);
     }
}
