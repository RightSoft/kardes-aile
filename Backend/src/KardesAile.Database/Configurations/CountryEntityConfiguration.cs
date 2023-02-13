using KardesAile.Database.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KardesAile.Database.Configurations;

public class CountryEntityConfiguration : CreatableEntityConfigurationBase<Country>
{
    public override void Configure(EntityTypeBuilder<Country> builder)
    {
        base.Configure(builder);
        builder.Property(e => e.Name).HasMaxLength(100).IsRequired();
        builder.Property(e => e.CountryCode).HasMaxLength(3).IsRequired();
        builder.HasIndex(x => x.Name)
            .IsUnique();
    }
}