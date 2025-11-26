using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ParkingApi.Domain.Entities;

namespace ParkingApi.Infrastructure.Data.Configurations;
public class ParkingSpaceConfiguration : IEntityTypeConfiguration<ParkingSpace>
{
    public void Configure(EntityTypeBuilder<ParkingSpace> builder)
    {
        builder.Property(t => t.Id)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(t => t.SpaceNumber)
            .HasMaxLength(200)
            .IsRequired();

        builder.OwnsOne(ps => ps.LicensePlate);

        builder.HasOne(ps => ps.Parking);
    }
}
