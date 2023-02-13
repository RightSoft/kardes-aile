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
            .WithMany(x => x.Matches)
            .IsRequired()
            .HasForeignKey(x => x.SupporterId);

        builder.HasOne(x => x.Victim)
            .WithMany(x => x.Matches)
            .IsRequired()
            .HasForeignKey(x => x.VictimId);

        builder.HasOne(x => x.VictimChild)
            .WithOne(x => x.VictimMatch)
            .HasForeignKey<Match>(x => x.VictimChildId);

        builder.HasOne(x => x.SupporterChild)
            .WithOne(x => x.SupporterMatch)
            .HasForeignKey<Match>(x => x.SupporterChildId);
    }
}