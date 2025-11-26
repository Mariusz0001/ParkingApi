using System.Reflection;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ParkingApi.Application.Common.Interfaces;
using ParkingApi.Domain.Entities;
using ParkingApi.Infrastructure.Identity;

namespace ParkingApi.Infrastructure.Data;
public class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IApplicationDbContext
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
