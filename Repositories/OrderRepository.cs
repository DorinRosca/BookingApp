using System.Collections.Generic;
using System.Runtime.CompilerServices;
using AutoMapper;
using Car_Booking_App.Entities;
using CarBookingApp.Data;
using CarBookingApp.Interfaces;
using CarBookingApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

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
               if (modelView == null)
               {
                    throw new ArgumentNullException(nameof(modelView), "the model cannot be null");
               }
               modelView =  SetDefaultStatus(modelView);
               modelView =  GetTotalAmount(modelView);
               var isAvailable = CarIsAvailable(modelView).Result;
               if (!isAvailable)
               {
                    return false;
               }

               var model = _mapper.Map<Order>(modelView);

                    await _context.Order.AddAsync(model);
                    var result = await _context.SaveChangesAsync();
                    return result > 0;
          }

          public OrderViewModel SetDefaultStatus(OrderViewModel model)
          {
               var processing = _context.Status.FirstOrDefault(s => s.StatusName == "Processing");
               model.StatusId = processing.StatusId;
               return model;
          }

          public OrderViewModel GetTotalAmount(OrderViewModel model)
          {
               var car = _context.Car.FirstOrDefault(c => c.CarId == model.CarId);

               int daysDifference = (int)(model.RentalEndDate.Date - model.RentalStartDate.Date).TotalDays;
               model.TotalAmount= (double)car.PricePerDay * (double)daysDifference;
               return model;
          }

          public async Task<IEnumerable<OrderViewModel>> GetOrders()
          {
               var query = await _context.Order
                    .Include(o => o.Car)
                    .Include(o => o.User)
                    .Include(o => o.Status)
                    .ToListAsync();
               var orders = query.Select(entity => _mapper.Map<OrderViewModel>(entity));
               return orders;
          }

          public async Task<IEnumerable<OrderViewModel>> GetUserOrders(string userId)
          {
               if (userId == null)
               {
                    throw new ArgumentNullException(nameof(userId), "UserId cannot be null");
               }
               var query = _context.Order
                    .Include(o => o.Car)
                    .Include(o => o.User)
                    .Include(o => o.Status)
                    .Where(c => c.UserId.Contains(userId));

               var orders = await query.ToListAsync();

               var orderViewModels = orders.Select(entity => _mapper.Map<OrderViewModel>(entity));
               return orderViewModels;
          }


          public async Task<bool> ConfirmOrder(int id)
          {
               if (id == 0)
               {
                    throw new ArgumentNullException(nameof(id), "The Id cannot be zero");
               }
               var order = await _context.Order.FirstOrDefaultAsync(o => o.OrderId == id);
               if (order == null)
               {
                    throw new ArgumentNullException(nameof(id), "There is no order with such Id");
               }
               
               var confirmedStatus = await _context.Status.FirstOrDefaultAsync(s => s.StatusName == "Confirmed");
               if (confirmedStatus != null)
               {
                    order.StatusId = confirmedStatus.StatusId; // Assign the ID of the "Confirmed" status
                    var result = await _context.SaveChangesAsync();
                    return result > 0;
               }
               else
               {
                    throw new ArgumentNullException(nameof(id), "There is no confirmed status");

               }
          }

          public async Task<bool> CancelOrder(int id)
          {
               if (id == 0)
               {
                    throw new ArgumentNullException(nameof(id), "The Id cannot be zero");
               }
               var order = await _context.Order.FirstOrDefaultAsync(o => o.OrderId == id);
               if (order == null)
               {
                    throw new ArgumentNullException(nameof(id), "There is no order with such Id");
               }
               var cancelledStatus = await _context.Status.FirstOrDefaultAsync(s => s.StatusName == "Cancelled");
               if (cancelledStatus != null)
               { 
                    order.StatusId = cancelledStatus.StatusId; // Assign the ID of the "Confirmed" status
                    var result = await _context.SaveChangesAsync();
                    return result > 0;
               }
               else
               {
                    throw new ArgumentNullException(nameof(id), "There is no Cancelled status");

               }
          }

          public async Task<bool> CarIsAvailable(OrderViewModel model)
          {
               int carId = model.CarId;
               DateTime rentalStartDate = model.RentalStartDate;
               DateTime rentalEndDate = model.RentalEndDate;

               int cancelledStatusId = await _context.Status
                    .Where(status => status.StatusName == "cancelled")
                    .Select(status => status.StatusId)
                    .FirstOrDefaultAsync();

               bool isAvailable = await _context.Order
                    .AnyAsync(order =>
                         order.CarId == carId &&
                         (
                              (order.RentalStartDate <= rentalStartDate && order.RentalEndDate >= rentalStartDate) ||  // Order starts within the rental period
                              (order.RentalStartDate <= rentalEndDate && order.RentalEndDate >= rentalEndDate) ||      // Order ends within the rental period
                              (order.RentalStartDate >= rentalStartDate && order.RentalEndDate <= rentalEndDate)      // Order completely overlaps the rental period
                         ) &&
                         order.StatusId != cancelledStatusId
                    );

               return !isAvailable;
          }




     }
}
