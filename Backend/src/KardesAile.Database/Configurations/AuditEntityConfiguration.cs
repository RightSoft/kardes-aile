using KardesAile.Database.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KardesAile.Database.Configurations;

public class AuditEntityConfiguration : CreatableEntityConfigurationBase<Audit>
{
    public override void Configure(EntityTypeBuilder<Audit> builder)
    {
        base.Configure(builder);
        builder.Property(x => x.Type).IsRequired();
        builder.Property(x => x.Action).HasMaxLength(255).IsRequired();
    }
}