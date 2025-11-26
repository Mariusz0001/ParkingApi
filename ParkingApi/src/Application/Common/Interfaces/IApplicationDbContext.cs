using ParkingApi.Domain.Entities;

namespace ParkingApi.Application.Common.Interfaces;
public interface IApplicationDbContext
{
    DbSet<Domain.Aggregates.Parking> Parkings { get; }
    DbSet<ParkingSpace> ParkingPlaces { get; }

    DbSet<TodoList> TodoLists { get; }

    DbSet<TodoItem> TodoItems { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
