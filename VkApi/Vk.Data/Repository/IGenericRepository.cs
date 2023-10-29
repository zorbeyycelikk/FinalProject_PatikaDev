using System.Linq.Expressions;
using Vk.Base.Model;

namespace Vk.Data.Repository;

public interface IGenericRepository<TEntity> where TEntity : BaseModel
{
    // Read Operations
    IQueryable<TEntity> GetAll(bool tracking = true); // TEntity'nin elemanlarını barındıran bir list döner.

    IQueryable<TEntity> GetWhere(Expression<Func<TEntity, bool>> method , bool tracking = true); //

    Task<TEntity> GetSingleAsync(Expression<Func<TEntity, bool>> method , bool tracking = true); //

    Task<TEntity> GetByIdAsync(string id,bool tracking = true);
    
    // Write Operations

    Task<TEntity> AddAsync(TEntity entity);
    
    Task<bool> AddRangeAsync(List<TEntity> entities);
    
    Task<bool> Remove(TEntity entity);
    
    Task<bool> Remove(string id);
    
    bool RemoveRange(List<TEntity> entities);
    
    bool Update(TEntity entity);

}