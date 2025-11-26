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

        builder.HasMany(p => p.ParkingSpaces)
                .WithOne(ps => ps.Parking)
                .HasForeignKey(ps => ps.ParkingId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);


        builder.Navigation(p => p.ParkingSpaces)
            .HasField("_parkingSpaces")
            .UsePropertyAccessMode(PropertyAccessMode.Field);
    }
}
