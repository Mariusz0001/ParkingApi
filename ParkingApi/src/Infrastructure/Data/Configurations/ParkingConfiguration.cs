using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ParkingApi.Infrastructure.Data.Configurations;
public class ParkingConfiguration : IEntityTypeConfiguration<Domain.Aggregates.Parking>
{
    public void Configure(EntityTypeBuilder<Domain.Aggregates.Parking> builder)
    {
        builder.Property(t => t.Id)
            .HasMaxLength(200)
            .IsRequired();

        builder
            .HasMany(b => b.ParkingSpaces);
    }
}
