namespace KardesAile.Database.Abstracts;

public interface IRepository<T> where T : class, IEntity
{
    IQueryable<T> AsQueryable { get; }
    void Update(T entity);
    void UpdateRange(IEnumerable<T> entityList);
    void Add(T entity);
    void AddRange(IEnumerable<T> entityList);
    void Delete(T entity);
    void DeleteRange(IEnumerable<T> entityList);
    Task<bool> DeleteById(int id, CancellationToken cancellationToken = default);
    ValueTask<T?> FindById(int id, CancellationToken cancellationToken = default);
}