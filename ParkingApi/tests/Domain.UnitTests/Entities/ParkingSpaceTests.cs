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
    [TestCase(VehicleType.SmallCar, 10, 3.00d)] // 10*0.1=1 + (floor(10/5)*1=2) = 3.00
    [TestCase(VehicleType.MediumCar, 5, 2.00d)] // 5*0.2=1 + (floor(5/5)*1=1) = 2.00
    [TestCase(VehicleType.MediumCar, 15, 6.00d)] // 15*0.2=3 + (floor(15/5)*1=3) = 6.00
    [TestCase(VehicleType.LargeCar, 5, 3.00d)] // 5*0.4=2 + (floor(5/5)*1=1) = 3.00
    public void ChargeVehicle_CalculatesCorrectTotalCharge(VehicleType type, int minutesParked, double expectedChargeDouble)
    {
        var timeIn = new DateTime(2025, 1, 1, 10, 0, 0, DateTimeKind.Utc);
        var timeOut = timeIn.AddMinutes(minutesParked);
        var space = CreateOccupiedSpaceForTest(type, timeIn);
        double expectedCharge = (double)expectedChargeDouble;

        var actualCharge = space.ChargeVehicle(timeOut);

        actualCharge.Should().Be(expectedCharge);
    }

    [Test]
    public void ChargeVehicle_WhenSpaceIsNotOccupied_ShouldThrowInvalidOperationException()
    {
        var space = new ParkingSpace(Guid.NewGuid(), Guid.NewGuid(), 1);

        Action act = () => space.ChargeVehicle(DateTime.UtcNow);

        act.Should().Throw<InvalidOperationException>()
           .WithMessage("*not occupied*");
    }

    [Test]
    public void ChargeVehicle_WhenTimeOutIsBeforeTimeIn_ShouldThrowArgumentException()
    {
        var timeIn = DateTime.UtcNow;
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
