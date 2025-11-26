namespace ParkingApi.Domain.Aggregates;
public class Parking
{
    public Guid Id { get; private set; }
    public int AvailableSpaces { get; private set; }
    public int OccupiedSpaces { get; private set; }

    private readonly List<ParkingSpace> _parkingSpaces = new();

    public IReadOnlyCollection<ParkingSpace> ParkingSpaces => _parkingSpaces.AsReadOnly();

    //for ef-core
    protected Parking()
    {
    }

    public Parking(int numberOfSpaces)
    {
        Id = Guid.NewGuid();
        AvailableSpaces = numberOfSpaces;
        OccupiedSpaces = 0;

        if (numberOfSpaces <= 0)
            throw new ArgumentException("A parking facility must have spaces.");

        for (int i = 1; i <= numberOfSpaces; i++)
        {
            _parkingSpaces.Add(new ParkingSpace(Guid.NewGuid(), Id, i));
        }
    }

    public ParkingSpace AssignVehicleToSpace(string? licensePlate, int vehType)
    {
        var availableSpace = _parkingSpaces
                .Where(s => !s.IsOccupied)
                .OrderBy(s => s.SpaceNumber)
                .FirstOrDefault() ?? throw new InvalidOperationException("The parking facility is currently full.");

        if (string.IsNullOrWhiteSpace(licensePlate))
            throw new ArgumentNullException(nameof(licensePlate), "License plate cannot be null.");

        availableSpace.Occupy(new LicensePlate(licensePlate), vehType, DateTime.UtcNow);

        OccupiedSpaces++;
        AvailableSpaces--;

        return availableSpace;
    }

    public (double Charge, DateTime? TimeIn, DateTime TimeOut) RemoveVehicleFromSpace(string licensePlate)
    {
        var timeOut = DateTime.UtcNow;
        var occupiedSpace = _parkingSpaces.FirstOrDefault(s => s.IsOccupied && s.LicensePlate?.Value == licensePlate);

        if (occupiedSpace is null)
            throw new InvalidOperationException($"Vehicle with plate {licensePlate} was not found.");

        var timeIn = occupiedSpace.TimeIn;

        var charge = occupiedSpace.ChargeVehicle(timeOut);
        occupiedSpace.Vacate();

        return new(charge, timeIn, timeOut);
    }
}
