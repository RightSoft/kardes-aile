using KardesAile.Database.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KardesAile.Database.Configurations;

public class MatchEntityConfiguration : ModifiableEntityConfigurationBase<Match>
{
    public override void Configure(EntityTypeBuilder<Match> builder)
    {
        base.Configure(builder);

        builder.Property(e => e.Active).IsRequired();
        builder.HasOne(x => x.Supporter)
            .WithOne(x => x.Match)
            .IsRequired()
            .HasForeignKey<Match>(x => x.SupporterId);

        builder.HasOne(x => x.Victim)
            .WithOne(x => x.Match)
            .IsRequired()
            .HasForeignKey<Match>(x => x.VictimId);

        builder.HasOne(x => x.VictimChild)
            .WithOne(x => x.VictimMatch)
            .HasForeignKey<Match>(x => x.VictimChildId);

        builder.HasOne(x => x.SupporterChild)
            .WithOne(x => x.SupporterMatch)
            .HasForeignKey<Match>(x => x.SupporterChildId);
    }
}