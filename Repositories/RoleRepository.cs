using AutoMapper;
using CarBookingApp.Data;
using CarBookingApp.Interfaces;
using CarBookingApp.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CarBookingApp.Repositories
{
     public class RoleRepository :IRole
     {
          private readonly DataContext _context;
          private readonly IMapper _mapper;

          public RoleRepository(DataContext context, IMapper mapper)
          {
               this._context = context;
               this._mapper = mapper;
          }
          public async Task<IEnumerable<RoleViewModel>> GetAllRoles()
          {
               var list = await _context.Roles.ToListAsync();
               var result = list.Select(entity => _mapper.Map<RoleViewModel>(entity));
               return result;
          }

          public async Task<bool> AddRole(RoleViewModel model)
          {
               if (model == null)
               {
                    throw new ArgumentNullException(nameof(model),"Model cannot be null");
               }

               model.NormalizedName = model.Name;
               var entity = _mapper.Map<RoleViewModel, IdentityRole>(model);
               entity.Id = Guid.NewGuid().ToString(); // Generate a new unique identifier for the Id
               await _context.Roles.AddAsync(entity);
               var result = await _context.SaveChangesAsync();
               return result > 0;

          }

          public async Task<bool> EditRole(RoleViewModel model)
          {
               if (model == null)
               {
                    throw new ArgumentNullException(nameof(model), "Model cannot be null");
               }

               var entity = _mapper.Map<RoleViewModel, IdentityRole>(model);
               entity.NormalizedName = entity.Name;
               _context.Entry(entity).State = EntityState.Modified;
               var result = await _context.SaveChangesAsync();
               return result > 0;
          }

          public async Task<bool> DeleteRole(string id)
          {
               if (id == null)
               {
                    throw new ArgumentNullException(nameof(id), "The Id cannot be null");
               }

               var entity = await _context.Roles.FindAsync(id);
               if (entity == null)
               {
                    throw new ArgumentNullException(nameof(id), "There is no entity with such Id");
               }
               _context.Roles.Remove(entity);
               var result = await _context.SaveChangesAsync();

               return result > 0;
          }

          public async Task<RoleViewModel> GetRole(string id)
          {
               if (id is null)
               {
                    throw new ArgumentNullException(nameof(id), "The Id cannot be null");
               }

               var entity = await _context.Roles.FindAsync(id);
               if (entity == null)
               {
                    throw new ArgumentNullException(nameof(entity), "There is no Role with such Id");
               }

               var model = _mapper.Map<RoleViewModel>(entity);

               return model;
          }
     }
}
