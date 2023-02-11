using KardesAile.Database.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KardesAile.Database.Configurations;

public class CocukEntityConfiguration : ModifiableEntityConfigurationBase<Cocuk>
{
    public override void Configure(EntityTypeBuilder<Cocuk> builder)
    {
        base.Configure(builder);
        builder.Property(x => x.Ad).HasMaxLength(50).IsRequired();
        builder.Property(x => x.Cinsiyet).IsRequired();
        builder.Property(x => x.DogumTarih).IsRequired();

        builder.HasOne(x => x.User)
            .WithMany(x => x.Cocuklar)
            .HasForeignKey(x => x.UserId);
    }
}