using System.Linq.Expressions;
using Vk.Base.Model;

namespace Vk.Data.Repository;

public interface IGenericRepository<TEntity> where TEntity : BaseModel
{
    // Read Operations
    IQueryable<TEntity> GetAll(); // TEntity'nin elemanlarını barındıran bir list döner.

    IQueryable<TEntity> GetWhere(Expression<Func<TEntity, bool>> method); //

    Task<TEntity> GetSingleAsync(Expression<Func<TEntity, bool>> method); //

    Task<TEntity> GetByIdAsync(int id);
    
    // Write Operations

    Task<TEntity> AddAsync(TEntity entity);
    
    Task<bool> AddRangeAsync(List<TEntity> entities);
    
    Task<bool> Remove(TEntity entity);
    
    Task<bool> Remove(int id);
    
    bool RemoveRange(List<TEntity> entities);
    
    bool Update(TEntity entity);

}