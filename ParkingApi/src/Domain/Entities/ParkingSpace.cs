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

    public ParkingSpace(Guid id, Guid parkingId, int spaceNumber)
    {
        Id = id;
        ParkingId = parkingId;
        SpaceNumber = spaceNumber;
        IsOccupied = false;
    }

    public void Occupy(LicensePlate licensePlate, int vehicleType, DateTime? timeIn)
    {
        if (IsOccupied)
        {
            throw new InvalidOperationException("Space is already occupied.");
        }

        IsOccupied = true;
        VehicleType = vehicleType;
        LicensePlate = licensePlate;
        SpaceNumber = SpaceNumber;
        TimeIn = timeIn ?? DateTime.Now;
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

    public double ChargeVehicle(DateTime TimeOut)
    {
        if (!IsOccupied)
            throw new InvalidOperationException("Parking space is not occupied.");

        if (!VehicleType.HasValue)
            throw new InvalidOperationException("Vehicle type is not set.");

        if (TimeIn > TimeOut)
            throw new ArgumentException("TimeOut cannot be earlier than TimeIn.");

        var chargeRates = new ParkingChargeRates();
        var duration = TimeOut - (TimeIn ?? DateTime.Now);

        double ratePerMin = chargeRates.GetRate((VehicleType)VehicleType!.Value);
        double ratePer5Min = duration.Minutes / 5;

        int fiveMinuteIntervals = (int)Math.Floor(duration.Minutes / 5m);
        double additionalCharge = fiveMinuteIntervals * 1.0;

        var result = (ratePerMin * duration.Minutes) + additionalCharge;

        return result;
    }
}
