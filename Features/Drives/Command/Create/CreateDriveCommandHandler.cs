using CarBookingApp.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CarBookingApp.Features.Drives.Command.Create;

public class CreateDriveCommandHandler : IRequestHandler<CreateDriveCommand, bool>
{
     private readonly DataContext _context;
     public CreateDriveCommandHandler(DataContext context)
     {
          _context = context;
     }
     public async Task<bool> Handle(CreateDriveCommand request, CancellationToken cancellationToken)
     {
          var Drive = new Entities.Drive
          {
               DriveName = request.DriveName
          };
          await _context.Drive.AddAsync(Drive, cancellationToken);
          var result = await _context.SaveChangesAsync(cancellationToken);
          return result > 0;
     }
}