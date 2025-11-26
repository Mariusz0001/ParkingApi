using System.Collections.ObjectModel;

namespace ParkingApi.Domain.ValueObjects;

public class ParkingChargeRates
{
    private readonly Dictionary<VehicleType, double> _rates = new Dictionary<VehicleType, double>
        {
            { VehicleType.SmallCar, 0.10 },
            { VehicleType.MediumCar, 0.20 },
            { VehicleType.LargeCar, 0.40 }
        };

    public ReadOnlyDictionary<VehicleType, double> Rates => new ReadOnlyDictionary<VehicleType, double>(_rates);

    public ParkingChargeRates() { }

    public double GetRate(VehicleType type)
    {
        if (_rates.TryGetValue(type, out double rate))
        {
            return rate;
        }
        throw new KeyNotFoundException($"Rate not found for vehicle type: {type}");
    }
}
