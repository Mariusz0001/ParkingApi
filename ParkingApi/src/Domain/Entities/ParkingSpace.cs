namespace ParkingApi.Domain.Entities;
public class ParkingSpace
{
    public Guid Id { get; private set; }
    public string SpaceNumber { get; private set; } = null!;
    public bool IsOccupied { get; private set; }
    public LicensePlate LicensePlate { get; private set; } = null!;
    public int? VehicleType { get; private set; }
    public DateTime TimeIn { get; private set; }

    // Protected constructor for EF Core/persistence
    protected ParkingSpace() { }

    internal ParkingSpace(Guid id, string spaceNumber)
    {
        Id = id;
        SpaceNumber = spaceNumber;
        IsOccupied = false;
        TimeIn = DateTime.UtcNow;
    }

    internal void Occupy(LicensePlate licensePlate, int vehicleType)
    {
        if (IsOccupied)
        {
            throw new InvalidOperationException("Space is already occupied.");
        }
        IsOccupied = true;
        VehicleType = vehicleType;
        LicensePlate = licensePlate;
        SpaceNumber = SpaceNumber;
    }
}
