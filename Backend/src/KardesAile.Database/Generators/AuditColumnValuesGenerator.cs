using KardesAile.CommonTypes.Context;
using KardesAile.Database.Abstracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace KardesAile.Database.Generators;

public class AuditColumnValuesGenerator : IAuditColumnValuesGenerator
{
    private readonly DateTime _time;
    private readonly IUserContext _userContext;

    public AuditColumnValuesGenerator(IUserContext userContext)
    {
        _userContext = userContext ?? throw new ArgumentNullException(nameof(userContext));
        _time = DateTime.UtcNow;
    }

    public void GenerateValues(EntityEntry entityEntry)
    {
        if (entityEntry == null) throw new ArgumentNullException(nameof(entityEntry));
        switch (entityEntry.Entity)
        {
            case IEntity entity when entityEntry.State == EntityState.Added:
                GenerateValues(entity);
                break;
            case BaseEntity baseEntity when entityEntry.State == EntityState.Modified:
                baseEntity.ModifiedAt = _time;
                baseEntity.ModifiedBy = _userContext.Username;
                break;
        }
    }

    public void GenerateValues<T>(T entity) where T : IEntity
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));

        entity.CreatedAt = _time;
        entity.CreatedBy = _userContext.Username;
    }
}