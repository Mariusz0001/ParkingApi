namespace ParkingApi.Application.Common.Exceptions;

public class VehicleNotFoundInParkingException : Exception
{
    public VehicleNotFoundInParkingException(string msg) : base(msg) { }
}
