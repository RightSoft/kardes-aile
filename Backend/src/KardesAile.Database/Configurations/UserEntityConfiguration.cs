using KardesAile.Database.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KardesAile.Database.Configurations;

public class UserEntityConfiguration : ModifiableEntityConfigurationBase<User>
{
    public override void Configure(EntityTypeBuilder<User> builder)
    {
        base.Configure(builder);
        builder.Property(x => x.FirstName).HasMaxLength(100).IsRequired();
        builder.Property(x => x.LastName).HasMaxLength(100).IsRequired();
        builder.Property(x => x.Email).HasMaxLength(255).IsRequired();
        builder.Property(x => x.EmailValidated).IsRequired();
        builder.Property(x => x.Phone).HasMaxLength(30).IsRequired();
        builder.Property(x => x.PhoneValidated).IsRequired();
        builder.Property(x => x.Role).IsRequired();
        builder.Property(x => x.Status).IsRequired();
        builder.Property(x => x.Hash).HasMaxLength(255);
        builder.Property(x => x.Salt).HasMaxLength(255);

        builder.HasIndex(x => x.Email)
            .IsUnique();
    }
}