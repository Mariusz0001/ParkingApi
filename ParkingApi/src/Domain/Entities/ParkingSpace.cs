namespace ParkingApi.Domain.Entities;
public class ParkingSpace
{
    public Guid Id { get; private set; }
    public string? SpaceNumber { get; private set; }
    public bool IsOccupied { get; private set; }
    public LicensePlate? LicensePlate { get; private set; }
    public int? VehicleType { get; private set; }

    // Protected constructor for EF Core/persistence
    protected ParkingSpace() { }

    internal ParkingSpace(Guid id, string spaceNumber)
    {
        Id = id;
        SpaceNumber = spaceNumber;
        IsOccupied = false;
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
    }

    internal void Vacate()
    {
        if (!IsOccupied)
        {
            throw new InvalidOperationException("Space is already vacant.");
        }
        IsOccupied = false;
        LicensePlate = null;
    }
}
