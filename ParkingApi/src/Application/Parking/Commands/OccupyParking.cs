using ParkingApi.Application.Common.Exceptions;
using ParkingApi.Application.Common.Interfaces;

namespace ParkingApi.Application.Parking.Commands;
public record OccupyParkingCommand : IRequest<OccupyParkingResult>
{
    public string? VehicleReg { get; init; }
    public int VehicleType { get; init; }
}

public record OccupyParkingResult(string? VehicleReg, int SpaceNumber, DateTime TimeIn);

public class OccupyParkingCommandHandler : IRequestHandler<OccupyParkingCommand, OccupyParkingResult>
{
    private readonly IApplicationDbContext _context;

    public OccupyParkingCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<OccupyParkingResult> Handle(OccupyParkingCommand request, CancellationToken cancellationToken)
    {
        var parking = await _context.Parkings
                .Include(p => p.ParkingSpaces)
                .FirstOrDefaultAsync(cancellationToken: cancellationToken)
                ?? throw new MainParkingNotFoundException();

        var availableSpace = parking.AssignVehicleToSpace(request.VehicleReg, request.VehicleType);

        await _context.SaveChangesAsync(cancellationToken);

        return new OccupyParkingResult(availableSpace.LicensePlate?.Value,
                                 availableSpace.SpaceNumber,
                                 availableSpace?.TimeIn ?? DateTime.Now);
    }
}
