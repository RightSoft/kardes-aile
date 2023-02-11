using KardesAile.Database.Generators;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;

namespace KardesAile.Database.Abstracts;

public class UnitOfWork : IUnitOfWork
{
    private readonly IAuditColumnValuesGenerator _auditColumnValuesGenerator;
    private readonly KardesAileDbContext _dbContext;
    private readonly IServiceProvider _serviceProvider;

    public UnitOfWork(
        IAuditColumnValuesGenerator auditColumnValuesGenerator,
        KardesAileDbContext dbContext,
        IServiceProvider serviceProvider
    )
    {
        _auditColumnValuesGenerator = auditColumnValuesGenerator ??
                                      throw new ArgumentNullException(nameof(auditColumnValuesGenerator));
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
    }

    public IRepository<T> GetRepository<T>() where T : class, IEntity
    {
        return _serviceProvider.GetRequiredService<IRepository<T>>();
    }

    public Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default)
    {
        return _dbContext.Database.BeginTransactionAsync(cancellationToken);
    }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        ProcessAudits();
        return _dbContext.SaveChangesAsync(cancellationToken);
    }

    public void Dispose()
    {
        _dbContext.Dispose();
    }

    private void ProcessAudits()
    {
        _dbContext.ChangeTracker.DetectChanges();
        foreach (var entry in _dbContext.ChangeTracker.Entries()
                     .Where(p =>
                         p.State is EntityState.Added or EntityState.Modified or EntityState.Deleted))
        {
            // Generates CreatedAt, CreatedBy, ModifiedAt, ModifiedBy column values
            _auditColumnValuesGenerator.GenerateValues(entry);
        }
    }
}