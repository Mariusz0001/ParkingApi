using ParkingApi.Application.Common.Exceptions;
using ParkingApi.Application.Common.Interfaces;

namespace ParkingApi.Application.Parking.Queries;
public record GetParkingStatsQuery : IRequest<GetParkingStatsDto>
{
}

public class GetParkingStatsQueryHandler : IRequestHandler<GetParkingStatsQuery, GetParkingStatsDto?>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetParkingStatsQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<GetParkingStatsDto?> Handle(GetParkingStatsQuery request, CancellationToken cancellationToken)
    {
        var parking = _context.Parkings
          .Include(p => p.ParkingSpaces)
          .AsQueryable()
          ?? throw new MainParkingNotFoundException();

        return await parking.ProjectTo<GetParkingStatsDto>(_mapper.ConfigurationProvider).FirstOrDefaultAsync(cancellationToken: cancellationToken);
    }
}
