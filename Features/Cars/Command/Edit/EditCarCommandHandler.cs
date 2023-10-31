using AutoMapper;
using CarBookingApp.Data;
using CarBookingApp.Features.Cars.Entities;
using MediatR;

namespace CarBookingApp.Features.Cars.Command.Edit
{
    public class EditCarCommandHandler : IRequestHandler<EditCarCommand, bool>
     {
          private readonly DataContext _context;
          private readonly IMapper _mapper;

          public EditCarCommandHandler(DataContext context, IMapper mapper)
          {
               _context = context;
               _mapper = mapper;
          }

          public async Task<bool> Handle(EditCarCommand request, CancellationToken cancellationToken)
          {
               if (request == null)
               {
                    throw new ArgumentNullException(nameof(request), "There model cannot be null");
               }

               if (request.ImageFile != null && request.ImageFile.Length > 0)
               {
                    using (var memoryStream = new MemoryStream())
                    {
                         request.ImageFile.CopyTo(memoryStream);
                         request.Image = memoryStream.ToArray();
                    }
               }
               var model = _mapper.Map<Car>(request);

                    _context.Car.Update(model);
                    var result = await _context.SaveChangesAsync(cancellationToken);
                    return result > 0;
          }
     }
}
