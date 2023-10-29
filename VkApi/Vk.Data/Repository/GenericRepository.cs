using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Vk.Base.Model;
using Vk.Data.Context;

namespace Vk.Data.Repository;

public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseModel
{
    
    private readonly VkDbContext dbContext;

    public GenericRepository(VkDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public DbSet<TEntity> Table => dbContext.Set<TEntity>();
    
    // Read Operations
    
    public IQueryable<TEntity> GetAll(bool tracking = true)
    {
        var query = Table.AsQueryable();
        if (!tracking) // tracking == false
        {
           query = query.AsNoTracking();
        }
        return query;
    }

    public IQueryable<TEntity> GetWhere(Expression<Func<TEntity, bool>> method,bool tracking = true)
    {
        var query = Table.Where(method);
        if (!tracking) // tracking == false
        {
            query = query.AsNoTracking();
        }
        return query;
    }

    public async Task<TEntity> GetSingleAsync(Expression<Func<TEntity, bool>> method,bool tracking = true)
    {
        var query = Table.AsQueryable();
        if (!tracking) // tracking == false
        {
            query = query.AsNoTracking();
        }
        return await query.FirstOrDefaultAsync(method);
    }
    
    public async Task<TEntity> GetByIdAsync(string id,bool tracking = true)
    {
        
        var query = Table.AsQueryable();
        
        if (!tracking) // tracking == false
        {
            query = query.AsNoTracking();
        }
        
        return await query.FirstOrDefaultAsync(x => x.Id == Guid.Parse(id));
    }
    
    // Write Operations

    public async Task<TEntity> AddAsync(TEntity entity)
    {
        entity.InsertDate = DateTime.UtcNow;
        var response = await Table.AddAsync(entity);
        return response.Entity;
    }

    public async Task<bool> AddRangeAsync(List<TEntity> entities)
    {
        entities.ForEach(x =>
        {
            x.InsertDate = DateTime.UtcNow;
            x.IsActive = true;
        });
        await Table.AddRangeAsync(entities);
        return true;
    }

    public async Task<bool> Remove(TEntity entity)
    {
        entity.IsActive = false;
        var response =Table.Update(entity);
        return response.State == EntityState.Modified;
    }

    public async Task<bool> Remove(string id)
    {
        var entity = await Table.FindAsync(Guid.Parse(id));
        
        entity.IsActive = false;
        
        var response =Table.Update(entity);
        return response.State == EntityState.Modified;
    }

    public bool RemoveRange(List<TEntity> entities)
    {
        entities.ForEach(x =>
        {
            x.UpdateDate = DateTime.UtcNow;
            x.IsActive = false;
        });
        
        Table.UpdateRange(entities);
        return true;
    }

    public bool Update(TEntity entity)
    {
        entity.UpdateDate = DateTime.UtcNow;
        var response = Table.Update(entity);
        return response.State == EntityState.Modified;
    }
    
}