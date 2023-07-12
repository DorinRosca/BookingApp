using System.Runtime.CompilerServices;
using AutoMapper;
using Car_Booking_App.Entities;
using CarBookingApp.Data;
using CarBookingApp.Interfaces;
using CarBookingApp.ViewModels;

namespace CarBookingApp.Repositories
{
     public class OrderRepository : IOrder
     {
          private readonly DataContext _context;
          private readonly IMapper _mapper;

          public OrderRepository(DataContext context, IMapper mapper)
          {
               this._context = context;
               this._mapper = mapper;
          }
          public async Task<bool> CreateOrder(OrderViewModel modelView)
          {
               var model = _mapper.Map<Order>(modelView);
               await _context.Order.AddAsync(model);
               var result = await _context.SaveChangesAsync();
               return result > 0;
          }



     }
}
