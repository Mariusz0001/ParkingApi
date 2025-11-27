using System.Reflection;
using Microsoft.EntityFrameworkCore;
using ParkingApi.Application.Common.Interfaces;
using ParkingApi.Domain.Entities;

namespace ParkingApi.Infrastructure.Data;
public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Domain.Aggregates.Parking> Parkings => Set<Domain.Aggregates.Parking>();
    public DbSet<ParkingSpace> ParkingPlaces => Set<ParkingSpace>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
