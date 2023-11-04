using System.Linq.Expressions;
using Vk.Base;

namespace Vk.Data.Repository;

public interface IGenericRepository<TEntity> where TEntity : BaseModel
{
    // Get All
    Task<List<TEntity>> GetAll(CancellationToken cancellationToken ,params string[] includes);
    
    // Get By Id
    Task<TEntity> GetById(string id,CancellationToken cancellationToken,params string[] includes);
    
    IQueryable<TEntity> GetWhere(Expression<Func<TEntity, bool>> method,params string[] includes);
    
    IQueryable<TEntity> GetAsQueryable(params string[] includes);
    
    // Write Operations
    
    Task<bool> AddAsync(TEntity entity, CancellationToken cancellationToken);
    
    Task<bool> AddRangeAsync(List<TEntity> entities,CancellationToken cancellationToken);
    
    void Remove(TEntity entity);
    
    void Remove(string id);
    
    void Update(TEntity entity);
    
}