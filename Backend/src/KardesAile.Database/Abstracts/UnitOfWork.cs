using KardesAile.CommonTypes.Enums;
using KardesAile.CommonTypes.Errors;
using KardesAile.Database.Generators;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;

namespace KardesAile.Database.Abstracts;

public class UnitOfWork : IUnitOfWork
{
    private readonly IAuditContext _auditContext;
    private readonly IAuditColumnValuesGenerator _auditColumnValuesGenerator;
    private readonly KardesAileDbContext _dbContext;
    private readonly IServiceProvider _serviceProvider;

    public UnitOfWork(
        IAuditContext auditContext,
        IAuditColumnValuesGenerator auditColumnValuesGenerator,
        KardesAileDbContext dbContext,
        IServiceProvider serviceProvider
    )
    {
        _auditContext = auditContext ?? throw new ArgumentNullException(nameof(auditContext));
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

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        ProcessAudits();
        try
        {
            return await _dbContext.SaveChangesAsync(cancellationToken);
        }
        catch (DbUpdateException e)
            when (e.InnerException is PostgresException {SqlState: PostgresErrorCodes.UniqueViolation})
        {
            throw Errors.RecordAlreadyExists;
        }
    }

    public void Dispose()
    {
        _auditContext.Dispose();
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

#pragma warning disable CS8509
            var operationType = entry.State switch
#pragma warning restore CS8509
            {
                EntityState.Deleted => OperationTypes.Delete,
                EntityState.Modified => OperationTypes.Update,
                EntityState.Added => OperationTypes.Create
            };

            _auditContext.AddDetail(operationType, entry);
        }
    }
}