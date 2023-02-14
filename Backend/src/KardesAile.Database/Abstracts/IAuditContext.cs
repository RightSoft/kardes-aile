using KardesAile.CommonTypes.Enums;
using KardesAile.Database.Entities;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace KardesAile.Database.Abstracts;

public interface IAuditContext : IDisposable
{
    void Start(AuditTypes type, string action);
    void AddEffectedUser(User user);
    void AddDetail(OperationTypes operation, EntityEntry entityEntry);
}