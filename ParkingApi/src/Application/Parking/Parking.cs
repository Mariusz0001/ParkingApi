using ParkingApi.Application.Common.Exceptions;
using ParkingApi.Application.Common.Interfaces;

namespace ParkingApi.Application.Parking;
public record ParkingCommand : IRequest<ParkingResult>
{
    public string? VehicleReg { get; init; }
    public int VehicleType { get; init; }
}

public record ParkingResult(string? VehicleReg, string SpaceNumber, DateTime TimeIn);

public class ParkingCommandHandler : IRequestHandler<ParkingCommand, ParkingResult>
{
    private readonly IApplicationDbContext _context;

    public ParkingCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ParkingResult> Handle(ParkingCommand request, CancellationToken cancellationToken)
    {
        var parking = await _context.Parkings.FirstOrDefaultAsync(cancellationToken: cancellationToken) ?? throw new MainParkingNotFoundException();

        var availableSpace = parking.AssignVehicleToSpace(request.VehicleReg, request.VehicleType);

        await _context.SaveChangesAsync(cancellationToken);

        return new ParkingResult(availableSpace.LicensePlate.Value, availableSpace.SpaceNumber, availableSpace.TimeIn);
    }
}
