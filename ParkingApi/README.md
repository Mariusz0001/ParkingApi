# Parking Api

Parking Api is a .NET application used to manage and allocate parking spaces for vehicles. It is responsible for calculating parking fees based on a per-minute rate.
- The parking charges are:
    - Small Car - £0.10/minute (1)
    - Medium Car - £0.20/minute (2)
    - Large Car £0.40/minute (3)
    - Every 5 minutes an additional charge of £1 will be added
      
This application was created as a recruitment task for TDS.

### Endpoints
| Type | Route | Query Params | Body | Return | Notes |
| ---- | ----- | ------------ | ---- | ------ | ----- |
| POST | /parking | | {VehicleReg: string, VehicleType: int} | {VehicleReg: string, SpaceNumber: int, TimeIn: DateTime} | Parks a given vehicle in the first available space and returns the vehicle and its space number |
| GET | /parking | | | {AvailableSpaces: int, OccupiedSpaces: int} | Gets available and occupied number of spaces |
| POST | /parking/exit | | {VehicleReg: string} | {VehicleReg: string, VehicleCharge: double TimeIn: DateTime, TimeOut: DateTime} | Should free up this vehicles space and return its final charge from its parking time until now |


## Tech stack:
- .NET 8
- Entity Framework + In Memory Database
- Swagger
- Automapper
- FluentAssetions
- nUnit

## System Architecture Assumptions:
- DDD
- Clean Architecture
- CQRS


## Getting started:

Restore Dependencies
dotnet restore
```
dotnet restore
```

Build the Project
```
dotnet build
```

Run the Application from the project directory (where the .csproj is located):
```
dotnet run
```
