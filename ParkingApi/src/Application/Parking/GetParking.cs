using ParkingApi.Application.Common.Exceptions;
using ParkingApi.Application.Common.Interfaces;

namespace ParkingApi.Application.Parking;
public record GetParkingCommand : IRequest<GetParkingResult>
{
}

public record GetParkingResult(int AvailableSpaces, int OccupiedSpaces);

public class GetParkingCommandHandler : IRequestHandler<GetParkingCommand, GetParkingResult>
{
    private readonly IApplicationDbContext _context;

    public GetParkingCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<GetParkingResult> Handle(GetParkingCommand request, CancellationToken cancellationToken)
    {
        var parking = await _context.Parkings
                .Include(p => p.ParkingSpaces)
                .FirstOrDefaultAsync(cancellationToken: cancellationToken)
                ?? throw new MainParkingNotFoundException();

        await _context.SaveChangesAsync(cancellationToken);

        return new GetParkingResult(parking.AvailableSpaces, parking.OccupiedSpaces);
    }
}
