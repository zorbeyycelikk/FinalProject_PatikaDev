using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Vk.Base;
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

    
    public async Task<List<TEntity>> GetAll(CancellationToken cancellationToken, params string[] includes)
    {var query = Table.AsQueryable();
        if (includes.Any())
        {
            query = includes.Aggregate(query, (current, incl) => current.Include(incl));
        }
        return await query.ToListAsync(cancellationToken);
    }

    public async Task<TEntity> GetById(int id, CancellationToken cancellationToken, params string[] includes)
    {
        var query = Table.AsQueryable();
        if (includes.Any())
        {
            query = includes.Aggregate(query, (current, incl) => current.Include(incl));
        }
        return await query.FirstOrDefaultAsync(x => x.Id == id,cancellationToken);
    }
    
    public IQueryable<TEntity> GetWhere(Expression<Func<TEntity, bool>> method, params string[] includes)
    {
        var query = Table.AsQueryable();
        query.Where(method);
        if (includes.Any())
        {
            query = includes.Aggregate(query, (current, incl) => current.Include(incl));
        }
        return query;
    }

    public IQueryable<TEntity> GetAsQueryable(params string[] includes)
    {
        var query = Table.AsQueryable();
        if (includes.Any())
        {
            query = includes.Aggregate(query, (current, incl) => current.Include(incl));
        }
        return query;
    }

    public async Task<bool> AddAsync(TEntity entity, CancellationToken cancellationToken)
    {
        entity.InsertDate = DateTime.UtcNow;
        entity.IsActive = true;
        var response = await Table.AddAsync(entity,cancellationToken);
        return response.State == EntityState.Added;
    }

    public async Task<bool> AddRangeAsync(List<TEntity> entities,CancellationToken cancellationToken)
    {
        entities.ForEach(x =>
        {
            x.InsertDate = DateTime.UtcNow;
            x.IsActive = true;
        });
        await Table.AddRangeAsync(entities,cancellationToken);
        return true;
    }

    public void Remove(TEntity entity)
    {
        entity.IsActive = false;
        entity.UpdateDate = DateTime.UtcNow;
        Table.Update(entity);
    }

    public void Remove(int id)
    {
        var entity = Table.Find(id);
        entity.IsActive = false;
        entity.UpdateDate = DateTime.UtcNow;
        Table.Update(entity);
    }
    
    public void Update(TEntity entity)
    {
        entity.UpdateDate = DateTime.UtcNow;
        Table.Update(entity);
    }
}