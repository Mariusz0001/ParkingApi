using FluentAssertions;
using NUnit.Framework;
using ParkingApi.Domain.Aggregates;

namespace ParkingApi.Domain.UnitTests.Aggregates;
public class ParkingTests
{
    // Helper constants/values for testing AssignVehicleToSpace
    private const string TestLicensePlate = "ABC1234";
    private const int TestVehicleType = 1; // Assuming 1 means standard car

    // --- Constructor Tests ---

    [Test]
    public void Constructor_WithValidNumberOfSpaces_ShouldCreateParkingWithCorrectSpaceCount()
    {
        // Arrange
        int numberOfSpaces = 10;

        // Act
        var parking = new Parking(numberOfSpaces);

        // Assert
        parking.Should().NotBeNull();
        parking.Id.Should().NotBeEmpty();
        parking.ParkingSpaces.Should().HaveCount(numberOfSpaces);
        parking.ParkingSpaces.Should().AllSatisfy(s => s.IsOccupied.Should().BeFalse());
    }

    [TestCase(0)]
    [TestCase(-1)]
    public void Constructor_WithInvalidNumberOfSpaces_ShouldThrowArgumentException(int invalidSpaces)
    {
        // Act
        Action act = () => new Parking(invalidSpaces);

        // Assert
        act.Should().Throw<ArgumentException>()
           .WithMessage("A parking facility must have spaces.*");
    }

    // --- AssignVehicleToSpace Tests ---

    [Test]
    public void AssignVehicleToSpace_WhenSpacesAvailable_ShouldOccupyOneSpace()
    {
        // Arrange
        var parking = new Parking(2); // Start with 2 spaces
        int initialAvailableSpaces = parking.ParkingSpaces.Count(s => !s.IsOccupied);
        initialAvailableSpaces.Should().Be(2);

        // Act
        var assignedSpace = parking.AssignVehicleToSpace(TestLicensePlate, TestVehicleType);

        // Assert
        assignedSpace.Should().NotBeNull();
        assignedSpace.IsOccupied.Should().BeTrue();
        parking.ParkingSpaces.Count(s => !s.IsOccupied).Should().Be(1); // One space left
    }

    [Test]
    public void AssignVehicleToSpace_WhenParkingIsFull_ShouldThrowInvalidOperationException()
    {
        // Arrange
        var parking = new Parking(1); // Only 1 space

        // Occupy the first space
        parking.AssignVehicleToSpace("OCCUPIED1", 1);
        parking.ParkingSpaces.First().IsOccupied.Should().BeTrue();

        // Act
        // Attempt to occupy the second space (which doesn't exist/is full)
        Action act = () => parking.AssignVehicleToSpace(TestLicensePlate, TestVehicleType);

        // Assert
        act.Should().Throw<InvalidOperationException>()
           .WithMessage("The parking facility is currently full.");
    }

    [Test]
    public void AssignVehicleToSpace_WhenLicensePlateIsNull_ShouldThrowArgumentNullException()
    {
        // Arrange
        var parking = new Parking(1);
        string nullLicensePlate = "";

        // Act
        Action act = () => parking.AssignVehicleToSpace(nullLicensePlate, TestVehicleType);

        // Assert
        act.Should().Throw<ArgumentNullException>()
           .WithParameterName("licensePlate");
    }
}


