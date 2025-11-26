using ParkingApi.Domain.Aggregates;

namespace ParkingApi.Domain.Entities;
public class ParkingSpace
{
    public Guid Id { get; private set; }
    public Guid ParkingId { get; private set; }
    public int SpaceNumber { get; private set; }
    public bool IsOccupied { get; private set; }
    public LicensePlate? LicensePlate { get; private set; } = null!;
    public int? VehicleType { get; private set; }
    public DateTime? TimeIn { get; private set; }

    public Parking Parking { get; private set; } = null!;

    // Protected constructor for EF Core/persistence
    protected ParkingSpace() { }

    internal ParkingSpace(Guid id, Guid parkingId, int spaceNumber)
    {
        Id = id;
        ParkingId = parkingId;
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
        TimeIn = DateTime.UtcNow;
    }

    internal void Vacate()
    {
        if (!IsOccupied)
        {
            throw new InvalidOperationException("This parking space is already vacant.");
        }

        IsOccupied = false;

        LicensePlate = null;
        VehicleType = null;
        TimeIn = null;
    }
}
