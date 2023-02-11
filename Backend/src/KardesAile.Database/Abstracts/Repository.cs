using Microsoft.EntityFrameworkCore;

namespace KardesAile.Database.Abstracts;

public class Repository<T> : IRepository<T> where T : class, IEntity
{
    public Repository(KardesAileDbContext dbContext)
    {
        EntitySet = dbContext.Set<T>();
    }

    private DbSet<T> EntitySet { get; }

    public IQueryable<T> AsQueryable => EntitySet.AsQueryable();

    public void Update(T entity)
    {
        EntitySet.Update(entity);
    }

    public void UpdateRange(IEnumerable<T> entityList)
    {
        EntitySet.UpdateRange(entityList);
    }

    public void Add(T entity)
    {
        EntitySet.Add(entity);
    }

    public void AddRange(IEnumerable<T> entityList)
    {
        EntitySet.AddRange(entityList);
    }

    public void Delete(T entity)
    {
        EntitySet.Remove(entity);
    }

    public void DeleteRange(IEnumerable<T> entityList)
    {
        EntitySet.RemoveRange(entityList);
    }

    public async Task<bool> DeleteById(int id, CancellationToken cancellationToken = default)
    {
        var entity = await FindById(id, cancellationToken);
        if (entity == null) return false;
        Delete(entity);
        return true;
    }

    public ValueTask<T?> FindById(int id, CancellationToken cancellationToken = default)
    {
        return EntitySet.FindAsync(new object[] {id}, cancellationToken);
    }
}