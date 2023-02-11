using KardesAile.Database.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KardesAile.Database.Configurations;

public class SupporterEntityConfiguration : ModifiableEntityConfigurationBase<Supporter>
{
    public override void Configure(EntityTypeBuilder<Supporter> builder)
    {
        base.Configure(builder);
        builder.Property(e => e.Address).HasMaxLength(255);
        builder.HasOne(x => x.User)
            .WithMany(x => x.Supporters)
            .IsRequired()
            .HasForeignKey(x => x.UserId);
        builder.HasOne(x => x.Country)
            .WithMany(x => x.Supporters)
            .HasForeignKey(x => x.CountryId);
        builder.HasOne(x => x.City)
            .WithMany(x => x.Supporters)
            .HasForeignKey(x => x.CityId);
    }
}