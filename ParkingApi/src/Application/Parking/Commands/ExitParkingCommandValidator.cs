namespace ParkingApi.Application.Parking.Commands;
public class ExitParkingCommandValidator : AbstractValidator<ExitParkingCommand>
{
    public ExitParkingCommandValidator()
    {
        RuleFor(v => v.VehicleReg)
            .NotEmpty()
            .MaximumLength(200)
                .WithMessage("'{PropertyName}' must be provided.");
    }
}
