using KardesAile.Database.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KardesAile.Database.Configurations;

internal class ModeratorEntityConfiguration : ModifiableEntityConfigurationBase<Moderator>
{
    public override void Configure(EntityTypeBuilder<Moderator> builder)
    {
        base.Configure(builder);

        builder.Property(x => x.FullName).HasMaxLength(100).IsRequired();
        builder.Property(x => x.Email).HasMaxLength(255).IsRequired();
        builder.Property(x => x.Password).HasMaxLength(255).IsRequired();
        builder.Property(x => x.IsDeleted).IsRequired();

        builder.HasIndex(x => x.Email)
            .IsUnique();
    }
}
