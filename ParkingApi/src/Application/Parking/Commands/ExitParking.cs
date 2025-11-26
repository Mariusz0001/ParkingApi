using ParkingApi.Application.Common.Exceptions;
using ParkingApi.Application.Common.Interfaces;

namespace ParkingApi.Application.Parking.Commands;
public record ExitParkingCommand : IRequest<ExitParkingResult>
{
    public string? VehicleReg { get; init; }
}

public record ExitParkingResult(string? VehicleReg, double VehicleCharge, DateTime? TimeIn, DateTime TimeOut);

public class ExitParkingCommandHandler : IRequestHandler<ExitParkingCommand, ExitParkingResult>
{
    private readonly IApplicationDbContext _context;

    public ExitParkingCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ExitParkingResult> Handle(ExitParkingCommand request, CancellationToken cancellationToken)
    {
        var parking = await _context.Parkings
                .Include(p => p.ParkingSpaces)
                .FirstOrDefaultAsync(cancellationToken: cancellationToken)
                ?? throw new MainParkingNotFoundException();

        var result = parking.RemoveVehicleFromSpace(request.VehicleReg!);

        if (result.ParkingSpace is null)
            throw new VehicleNotFoundInParkingException(request.VehicleReg!);

        await _context.SaveChangesAsync(cancellationToken);

        return new ExitParkingResult(result.ParkingSpace.LicensePlate?.Value, result.Charge, result.ParkingSpace.TimeIn?.Value, result.TimeOut);
    }
}
