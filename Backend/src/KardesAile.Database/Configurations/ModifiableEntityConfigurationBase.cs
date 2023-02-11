using KardesAile.Database.Abstracts;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KardesAile.Database.Configurations;

public abstract class ModifiableEntityConfigurationBase<T> : CreatableEntityConfigurationBase<T> where T: BaseEntity
{
    public override void Configure(EntityTypeBuilder<T> builder)
    {
        base.Configure(builder);
        builder.Property(x => x.ModifiedAt);
        builder.Property(x => x.ModifiedBy)
            .HasMaxLength(255);
    }
}