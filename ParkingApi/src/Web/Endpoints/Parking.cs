using ParkingApi.Application.Parking.Commands;
using ParkingApi.Application.Parking.Queries;

namespace ParkingApi.Web.Endpoints;
public class Parking : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .MapPost(Occupy)
            .MapGet(Stats)
            .MapPost(Exit, nameof(Exit));
    }

    public Task<OccupyParkingResult> Occupy(ISender sender, OccupyParkingCommand command)
    {
        return sender.Send(command);
    }

    public Task<GetParkingStatsDto> Stats(ISender sender, [AsParameters] GetParkingStatsQuery query)
    {
        return sender.Send(query);
    }

    public Task<ExitParkingResult> Exit(ISender sender, ExitParkingCommand command)
    {
        return sender.Send(command);
    }
}
