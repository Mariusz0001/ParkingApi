using ParkingApi.Application.Parking.Commands;
using ParkingApi.Application.Parking.Queries;

namespace ParkingApi.Web.Endpoints;
public class Parking : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .MapPost(Occupy)
            .MapGet(Stats);
    }

    public Task<ParkingResult> Occupy(ISender sender, ParkingCommand command)
    {
        return sender.Send(command);
    }

    public Task<GetParkingStatsDto> Stats(ISender sender, [AsParameters] GetParkingStatsQuery query)
    {
        return sender.Send(query);
    }
}
