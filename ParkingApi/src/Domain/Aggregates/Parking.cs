namespace ParkingApi.Domain.Aggregates;
public class Parking
{
    public Guid Id { get; private set; }

    private readonly List<ParkingSpace> _parkingSpaces = new();

    public IReadOnlyCollection<ParkingSpace> ParkingSpaces => _parkingSpaces.AsReadOnly();

    //for ef-core
    protected Parking()
    {
    }

    public Parking(int numberOfSpaces)
    {
        Id = Guid.NewGuid();

        if (numberOfSpaces <= 0)
            throw new ArgumentException("A parking facility must have spaces.");

        for (int i = 1; i <= numberOfSpaces; i++)
        {
            _parkingSpaces.Add(new ParkingSpace(Guid.NewGuid(), $"{i}"));
        }
    }

    public ParkingSpace AssignVehicleToSpace(string? licensePlate, int vehType)
    {
        var availableSpace = _parkingSpaces.FirstOrDefault(s => !s.IsOccupied);

        if (availableSpace is null)
            throw new InvalidOperationException("The parking facility is currently full.");

        if (licensePlate is null)
            throw new ArgumentNullException(nameof(licensePlate), "License plate cannot be null.");

        availableSpace.Occupy(new LicensePlate(licensePlate), vehType);

        return availableSpace;
    }

    /*   public void RemoveVehicleFromSpace(string licensePlate)
       {
           var occupiedSpace = _parkingSpaces.FirstOrDefault(s => s.OccupyingVehicleLicensePlate == licensePlate);

           if (occupiedSpace is null)
               throw new InvalidOperationException($"Vehicle with plate {licensePlate} was not found.");

           occupiedSpace.Vacate();
       }*/
}
