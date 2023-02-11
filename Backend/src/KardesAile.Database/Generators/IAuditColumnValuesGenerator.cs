using KardesAile.Database.Abstracts;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace KardesAile.Database.Generators;

public interface IAuditColumnValuesGenerator
{
    void GenerateValues(EntityEntry entityEntry);
    void GenerateValues<T>(T entity) where T : IEntity;
}