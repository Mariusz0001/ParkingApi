namespace ParkingApi.Application.Common.Exceptions;

public class MainParkingNotFoundException : Exception
{
    public MainParkingNotFoundException() : base("Not provided main parking facility in system") { }
}
