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
    
    public IQueryable<TEntity> GetAll()
    {
        return Table;
    }

    public IQueryable<TEntity> GetWhere(Expression<Func<TEntity, bool>> method)
    {
        return Table.Where(method);
    }

    public async Task<TEntity> GetSingleAsync(Expression<Func<TEntity, bool>> method)
    {
        return await Table.FirstOrDefaultAsync(method);
    }

    public async Task<TEntity> GetByIdAsync(int id)
    {
        return await Table.FindAsync(id);
    }
    
    // Write Operations

    public async Task<TEntity> AddAsync(TEntity entity)
    {
        var response = await Table.AddAsync(entity);
        return response.Entity;
    }

    public async Task<bool> AddRangeAsync(List<TEntity> entities)
    {
        entities.ForEach(x =>
        {
            x.InsertUserId = 9999; // admin Id
            x.InsertDate = DateTime.UtcNow;
            x.IsActive = true;
        });
        await Table.AddRangeAsync(entities);
        return true;
    }

    public async Task<bool> Remove(TEntity entity)
    {
        entity.UpdateUserId = 9999; // admin Id
        entity.UpdateDate = DateTime.UtcNow;
        entity.IsActive = false;
        
        var response =Table.Update(entity);
        return response.State == EntityState.Modified;
    }

    public async Task<bool> Remove(int id)
    {
        var entity = await Table.FindAsync(id);
        
        entity.UpdateUserId = 9999; // admin Id
        entity.UpdateDate = DateTime.UtcNow;
        entity.IsActive = false;
        
        var response =Table.Update(entity);
        return response.State == EntityState.Modified;
    }

    public bool RemoveRange(List<TEntity> entities)
    {
        entities.ForEach(x =>
        {
            x.UpdateUserId = 9999; // admin Id
            x.UpdateDate = DateTime.UtcNow;
            x.IsActive = false;
        });
        Table.UpdateRange(entities);
        return true;
    }

    public bool Update(TEntity entity)
    {
        var response = Table.Update(entity);
        return response.State == EntityState.Modified;
    }
    
}