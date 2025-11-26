using ParkingApi.Application.Parking;

namespace ParkingApi.Web.Endpoints;
public class Parking : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .MapPost(Add);
    }

    public Task<ParkingResult> Add(ISender sender, ParkingCommand command)
    {
        return sender.Send(command);
    }
}
