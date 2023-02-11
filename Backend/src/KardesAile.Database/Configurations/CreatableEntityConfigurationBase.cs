using KardesAile.Database.Abstracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KardesAile.Database.Configurations;

public abstract class CreatableEntityConfigurationBase<T> : IEntityTypeConfiguration<T> where T : class, IEntity
{
    public virtual void Configure(EntityTypeBuilder<T> builder)
    {
        builder.Property(x => x.Id)
            .IsRequired()
            .ValueGeneratedNever();
        builder.HasKey(x => x.Id);
        builder.Property(x => x.CreatedAt)
            .IsRequired();
        builder.Property(x => x.CreatedBy)
            .HasMaxLength(255)
            .IsRequired();
        builder.Property(x => x.Version)
            .IsRowVersion();
    }
}