using System.Collections.ObjectModel;

namespace ParkingApi.Domain.ValueObjects;

public class ParkingChargeRates
{
    private readonly Dictionary<VehicleType, decimal> _rates = new Dictionary<VehicleType, decimal>
        {
            { VehicleType.SmallCar, 0.10m },
            { VehicleType.MediumCar, 0.20m },
            { VehicleType.LargeCar, 0.40m }
        };

    public ReadOnlyDictionary<VehicleType, decimal> Rates => new ReadOnlyDictionary<VehicleType, decimal>(_rates);

    public ParkingChargeRates() { }

    public decimal GetRate(VehicleType type)
    {
        if (_rates.TryGetValue(type, out decimal rate))
        {
            return rate;
        }
        throw new KeyNotFoundException($"Rate not found for vehicle type: {type}");
    }
}
