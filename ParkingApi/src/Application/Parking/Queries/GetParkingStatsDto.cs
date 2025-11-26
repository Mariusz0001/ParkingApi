namespace ParkingApi.Application.Parking.Queries;
public class GetParkingStatsDto
{
    public int AvailableSpaces { get; init; }
    public int OccupiedSpaces { get; init; }

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Domain.Aggregates.Parking, GetParkingStatsDto>();
        }
    }
}
