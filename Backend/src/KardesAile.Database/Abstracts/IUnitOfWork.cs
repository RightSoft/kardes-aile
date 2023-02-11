using KardesAile.Database.Entities;
using Microsoft.EntityFrameworkCore.Storage;

namespace KardesAile.Database.Abstracts;

public interface IUnitOfWork : IDisposable
{
    IRepository<User> User => GetRepository<User>();
    IRepository<Cocuk> Cocuk => GetRepository<Cocuk>();
    IRepository<T> GetRepository<T>() where T : class, IEntity;
    Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default);
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}