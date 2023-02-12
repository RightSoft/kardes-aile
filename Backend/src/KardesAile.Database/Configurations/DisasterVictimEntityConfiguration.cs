using KardesAile.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KardesAile.Database.Configurations;

public class DisasterVictimEntityConfiguration : ModifiableEntityConfigurationBase<DisasterVictim>
{
    public override void Configure(EntityTypeBuilder<DisasterVictim> builder)
    {
        base.Configure(builder);
        builder.Property(e => e.Address).HasMaxLength(255);
        builder.Property(x => x.AddressValidated).IsRequired();
        builder.Property(e => e.TemporaryAddress).HasMaxLength(255);
        builder.HasOne(x => x.User)
            .WithMany(x => x.DisasterVictims)
            .IsRequired()
            .HasForeignKey(x => x.UserId);
        builder.HasOne(x => x.Country)
            .WithMany(x => x.DisasterVictims)
            .HasForeignKey(x => x.CountryId);
        builder.Property(e => e.IdentityNumber).HasMaxLength(11);
        builder.Property(x => x.IdentityNumberValidated).IsRequired();

        builder.HasOne(x => x.City)
            .WithMany(x => x.DisasterVictimsCities)
            .HasForeignKey(x => x.CityId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(x => x.TemporaryCity)
            .WithMany(x => x.DisasterVictimsTemporaryCities)
            .HasForeignKey(x => x.TemporaryCityId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}