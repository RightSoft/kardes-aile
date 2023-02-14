using System.Text.Json;
using KardesAile.CommonTypes.Enums;
using KardesAile.Database.Abstracts;
using KardesAile.Database.Entities;
using KardesAile.Database.Generators;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace KardesAile.Business.Context;

public class AuditContext : IAuditContext
{
    private readonly IAuditColumnValuesGenerator _auditColumnValuesGenerator;
    private readonly IRepository<Audit> _auditRepository;
    private Audit? _audit;
    private bool _disposed;

    public AuditContext(IRepository<Audit> auditRepository, IAuditColumnValuesGenerator auditColumnValuesGenerator)
    {
        _auditRepository = auditRepository ?? throw new ArgumentNullException(nameof(auditRepository));
        _auditColumnValuesGenerator = auditColumnValuesGenerator ??
                                      throw new ArgumentNullException(nameof(auditColumnValuesGenerator));
    }

    public void Start(AuditTypes type, string action)
    {
        if (action == null) throw new ArgumentNullException(nameof(action));
        if (_disposed)
            throw new ObjectDisposedException("AuditContext");

        if (_audit != null) throw new InvalidOperationException("Already in audit scope");

        _audit = new Audit
        {
            Type = type,
            Action = action
        };

        _auditRepository.Add(_audit);
    }

    public void AddEffectedUser(User user)
    {
        if (user == null) throw new ArgumentNullException(nameof(user));
        if (_disposed)
            throw new ObjectDisposedException("AuditContext");

        if (_audit == null) throw new InvalidOperationException("Audit context needed");

        var effectedUser = new AuditEffectedUser
        {
            User = user
        };

        _audit.AuditEffectedUsers.Add(effectedUser);
    }

    public void AddDetail(OperationTypes operation, EntityEntry entityEntry)
    {
        if (entityEntry == null) throw new ArgumentNullException(nameof(entityEntry));
        if (_disposed)
            throw new ObjectDisposedException("AuditContext");

        if (_audit == null) throw new InvalidOperationException("Audit context needed");

        // Skip audit entities
        if (entityEntry.Metadata.ClrType == typeof(Audit) ||
            entityEntry.Metadata.ClrType == typeof(AuditDetail) ||
            entityEntry.Metadata.ClrType == typeof(AuditEffectedUser))
            return;

        if (entityEntry.Entity is not IEntity entity) return;

        object auditData = operation switch
        {
            OperationTypes.Update => GetModifiedPropertyValues(entityEntry),
            OperationTypes.Delete => GetOriginalPropertyValues(entityEntry),
            _ => GetCurrentPropertyValues(entityEntry)
        };

        var detail = new AuditDetail
        {
            EntityName = entityEntry.Metadata.DisplayName(),
            EntityId = entity.Id,
            Operation = operation,
            Data = JsonSerializer.Serialize(auditData)
        };

        _auditColumnValuesGenerator.GenerateValues(detail);

        _audit.AuditDetails.Add(detail);
    }

    public void Dispose()
    {
        _disposed = true;
        _audit = null;
    }

    private IEnumerable<PropertyValues> GetModifiedPropertyValues(
        EntityEntry entry)
    {
        var result = entry.Properties
            .Where(e => e.IsModified)
            .Select(property => new PropertyValues(property.Metadata.Name,
                property.OriginalValue,
                property.CurrentValue));
        return result;
    }

    private IEnumerable<PropertyValues> GetOriginalPropertyValues(
        EntityEntry entry)
    {
        var result = entry.Properties
            .Select(property => new PropertyValues(property.Metadata.Name,
                property.OriginalValue,
                null));
        return result;
    }

    private IEnumerable<PropertyValues> GetCurrentPropertyValues(
        EntityEntry entry)
    {
        var result = entry.Properties
            .Select(property => new PropertyValues(property.Metadata.Name,
                null,
                property.CurrentValue));
        return result;
    }
}