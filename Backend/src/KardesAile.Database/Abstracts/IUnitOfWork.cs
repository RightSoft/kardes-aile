using KardesAile.Database.Entities;
using Microsoft.EntityFrameworkCore.Storage;

namespace KardesAile.Database.Abstracts;

public interface IUnitOfWork : IDisposable
{
    IRepository<User> User => GetRepository<User>();
    IRepository<Child> Child => GetRepository<Child>();
    IRepository<City> City => GetRepository<City>();
    IRepository<Country> Country => GetRepository<Country>();
    IRepository<Supporter> Supporter => GetRepository<Supporter>();
    IRepository<Match> Match => GetRepository<Match>();
    IRepository<T> GetRepository<T>() where T : class, IEntity;
    Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default);
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}