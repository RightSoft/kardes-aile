using KardesAile.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KardesAile.Database.Configurations;

public class AuditDetailEntityConfiguration : CreatableEntityConfigurationBase<AuditDetail>
{
    public override void Configure(EntityTypeBuilder<AuditDetail> builder)
    {
        base.Configure(builder);
        builder.Property(x => x.AuditId).IsRequired();
        builder.Property(x => x.EntityId).IsRequired();
        builder.Property(x => x.EntityName).HasMaxLength(255).IsRequired();
        builder.Property(x => x.Data).IsRequired().HasColumnType("json");
        builder.Property(x => x.Operation).IsRequired();

        builder.HasOne(p => p.Audit)
            .WithMany(p => p.AuditDetails)
            .IsRequired()
            .HasForeignKey(p => p.AuditId);
    }
}