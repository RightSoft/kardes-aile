using KardesAile.Database.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KardesAile.Database.Configurations;

public class AuditEffectedUserEntityConfiguration : CreatableEntityConfigurationBase<AuditEffectedUser>
{
    public override void Configure(EntityTypeBuilder<AuditEffectedUser> builder)
    {
        base.Configure(builder);
        builder.Property(x => x.AuditId).IsRequired();
        builder.Property(x => x.UserId).IsRequired();

        builder.HasOne(p => p.Audit)
            .WithMany(p => p.AuditEffectedUsers)
            .IsRequired()
            .HasForeignKey(p => p.AuditId);

        builder.HasOne(p => p.User)
            .WithMany(p => p.AuditEffectedUsers)
            .IsRequired()
            .HasForeignKey(p => p.UserId);
    }
}