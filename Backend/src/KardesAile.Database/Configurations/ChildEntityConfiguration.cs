using KardesAile.Database.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KardesAile.Database.Configurations;

public class ChildEntityConfiguration : ModifiableEntityConfigurationBase<Child>
{
    public override void Configure(EntityTypeBuilder<Child> builder)
    {
        base.Configure(builder);
        builder.Property(x => x.Name).HasMaxLength(100).IsRequired();
        builder.Property(x => x.Gender).IsRequired();
        builder.Property(x => x.BirthDate).IsRequired();

        builder.HasOne(x => x.User)
            .WithMany(x => x.Children)
            .HasForeignKey(x => x.UserId);
    }
}