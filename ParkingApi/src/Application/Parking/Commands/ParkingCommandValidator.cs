namespace ParkingApi.Application.Parking.Commands;
public class ParkingCommandValidator : AbstractValidator<ParkingCommand>
{
    public ParkingCommandValidator()
    {
        RuleFor(v => v.VehicleReg)
            .NotEmpty()
            .MaximumLength(200)
                .WithMessage("'{PropertyName}' must be provided.");

        RuleFor(v => v.VehicleType)
            .GreaterThan(0)
            .LessThan(4)
                .WithMessage("'{PropertyName}' must be 1-3");
    }
}
