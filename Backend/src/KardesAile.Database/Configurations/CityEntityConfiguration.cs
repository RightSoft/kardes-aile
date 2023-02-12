using KardesAile.Database.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KardesAile.Database.Configurations;

public class CityEntityConfiguration : CreatableEntityConfigurationBase<City>
{
    public override void Configure(EntityTypeBuilder<City> builder)
    {
        base.Configure(builder);
        builder.Property(e => e.Name).HasMaxLength(100).IsRequired();
        builder.HasOne(x => x.Country)
            .WithMany(x => x.Cities)
            .IsRequired()
            .HasForeignKey(x => x.CountryId);
        builder.HasIndex(x => x.Name)
            .IsUnique();
        
        builder.HasMany(x => x.DisasterVictimsCities)
            .WithOne(x => x.City)
            .HasForeignKey(e => e.CityId);
        
        builder.HasMany(x => x.DisasterVictimsTemporaryCities)
            .WithOne(x => x.TemporaryCity)
            .HasForeignKey(e => e.TemporaryCityId);
    }
}