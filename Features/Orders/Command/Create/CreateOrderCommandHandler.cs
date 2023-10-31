using AutoMapper;
using CarBookingApp.Data;
using CarBookingApp.Features.Orders.Entities;
using CarBookingApp.Features.Orders.ViewModel;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CarBookingApp.Features.Orders.Command.Create
{
    public class CreateOrderCommandHandler :IRequestHandler<CreateOrderCommand, OrderViewModel>
     {
          private readonly IMapper _mapper;
          private readonly DataContext _context;

          public CreateOrderCommandHandler(IMapper mapper, DataContext context)
          {
               _mapper = mapper;
               _context = context;
          }

          public async Task<OrderViewModel> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
          {
               if (request == null)
               {
                    throw new ArgumentNullException(nameof(request), "the model cannot be null");
               }
               request =  SetDefaultStatus(request);
               request =  GetTotalAmount(request);
               var isAvailable = CarIsAvailable(request).Result;
               if (!isAvailable)
               {
                    throw new NullReferenceException();
               }

               var model = _mapper.Map<Order>(request);

               await _context.Order.AddAsync(model,cancellationToken);
               var result = await _context.SaveChangesAsync(cancellationToken);
               if (result > 0)
               {
                    return _mapper.Map<OrderViewModel>(model);
               }
               throw new NullReferenceException();
          }
          public CreateOrderCommand SetDefaultStatus(CreateOrderCommand model)
          {
               var processing = _context.Status.FirstOrDefault(s => s.StatusName == "Processing");
               if (processing != null) model.StatusId = processing.StatusId;
               return model;
          }

          public CreateOrderCommand GetTotalAmount(CreateOrderCommand model)
          {
               var car = _context.Car.FirstOrDefault(c => c.CarId == model.CarId);

               int daysDifference = (int)(model.RentalEndDate.Date - model.RentalStartDate.Date).TotalDays;
               model.TotalAmount= (double)car.PricePerDay * (double)daysDifference;
               return model;
          }
          public async Task<bool> CarIsAvailable(CreateOrderCommand model)
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
