using FluentAssertions;
using NUnit.Framework;
using ParkingApi.Domain.Entities;
using ParkingApi.Domain.ValueObjects;

namespace ParkingApi.Domain.UnitTests.Entities;
public class ParkingSpaceTests
{
    [Test]
    [TestCase(VehicleType.SmallCar, 1, 0.10d)]
    [TestCase(VehicleType.SmallCar, 4, 0.40d)]
    [TestCase(VehicleType.SmallCar, 5, 1.50d)]
    [TestCase(VehicleType.SmallCar, 6, 1.60d)]
    [TestCase(VehicleType.SmallCar, 9, 1.90d)]
    [TestCase(VehicleType.SmallCar, 10, 3.00d)]
    [TestCase(VehicleType.MediumCar, 5, 2.00d)]
    [TestCase(VehicleType.MediumCar, 15, 6.00d)]
    [TestCase(VehicleType.LargeCar, 5, 3.00d)]
    [TestCase(VehicleType.LargeCar, 12, 6.80d)]
    public void ChargeVehicle_CalculatesCorrectTotalCharge(VehicleType type, int minutesParked, double expectedChargeDouble)
    {
        var timeIn = new DateTime(2025, 1, 1, 10, 0, 0, DateTimeKind.Utc);
        var timeOut = timeIn.AddMinutes(minutesParked);
        var space = CreateOccupiedSpaceForTest(type, timeIn);
        double expectedCharge = (double)expectedChargeDouble;

        var actualCharge = space.ChargeVehicle(timeOut);

        actualCharge.Should().BeApproximately(expectedCharge, 0.001);
    }

    [Test]
    public void ChargeVehicle_WhenSpaceIsNotOccupied_ShouldThrowInvalidOperationException()
    {
        var space = new ParkingSpace(Guid.NewGuid(), Guid.NewGuid(), 1);

        Action act = () => space.ChargeVehicle(DateTime.Now);

        act.Should().Throw<InvalidOperationException>()
           .WithMessage("*not occupied*");
    }

    [Test]
    public void ChargeVehicle_WhenTimeOutIsBeforeTimeIn_ShouldThrowArgumentException()
    {
        var timeIn = DateTime.Now;
        var timeOut = timeIn.AddMinutes(-10);
        var space = CreateOccupiedSpaceForTest(VehicleType.SmallCar, timeIn);

        Action act = () => space.ChargeVehicle(timeOut);

        act.Should().Throw<ArgumentException>()
           .WithMessage("*earlier than TimeIn*");
    }

    private ParkingSpace CreateOccupiedSpaceForTest(VehicleType type, DateTime timeIn)
    {
        var space = new ParkingSpace(Guid.NewGuid(), Guid.NewGuid(), 1);

        space.Occupy(new LicensePlate("TEST123"), (int)type, timeIn);

        return space;
    }
}
