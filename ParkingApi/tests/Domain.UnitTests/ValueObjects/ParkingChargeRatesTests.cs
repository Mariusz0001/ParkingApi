using FluentAssertions;
using NUnit.Framework;
using ParkingApi.Domain.ValueObjects;

namespace ParkingApi.Domain.UnitTests.ValueObjects;
public class ParkingChargeRatesTests
{
    private ParkingChargeRates _sut;

    [SetUp]
    public void Setup()
    {
        _sut = new ParkingChargeRates();
    }

    [Test]
    public void Constructor_ShouldInitializeRatesDictionaryWithCorrectCounts()
    {
        _sut.Rates.Should().NotBeNull();
        _sut.Rates.Should().HaveCount(3);
    }

    [Test]
    public void RatesProperty_ShouldReturnReadOnlyDictionary()
    {
        var rates = _sut.Rates;

        rates.Should().BeAssignableTo<IReadOnlyDictionary<VehicleType, decimal>>();
    }

    [Test]
    public void GetRate_ForSmallCar_ShouldReturnCorrectRate()
    {
        var expectedRate = 0.10;

        var actualRate = _sut.GetRate(VehicleType.SmallCar);

        actualRate.Should().Be(expectedRate);
    }

    [Test]
    public void GetRate_ForMediumCar_ShouldReturnCorrectRate()
    {
        var expectedRate = 0.2;

        var actualRate = _sut.GetRate(VehicleType.MediumCar);

        actualRate.Should().Be(expectedRate);
    }

    [Test]
    public void GetRate_ForLargeCar_ShouldReturnCorrectRate()
    {
        var expectedRate = 0.40;

        var actualRate = _sut.GetRate(VehicleType.LargeCar);

        actualRate.Should().Be(expectedRate);
    }

    [Test]
    public void GetRate_ForInvalidVehicleType_ShouldThrowKeyNotFoundException()
    {
        var invalidType = (VehicleType)999;

        Action act = () => _sut.GetRate(invalidType);

        act.Should().Throw<KeyNotFoundException>()
           .WithMessage($"Rate not found for vehicle type: {invalidType}");
    }
}
