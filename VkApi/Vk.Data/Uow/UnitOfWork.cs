using Vk.Data.Context;
using Vk.Data.Domain;
using Vk.Data.Repository;

namespace Vk.Data.Uow;

public class UnitOfWork : IUnitOfWork
{
    private readonly VkDbContext dbContext;

    public UnitOfWork(VkDbContext dbContext)
    {
        this.dbContext = dbContext;

        CustomerRepository = new GenericRepository<Customer>(dbContext);
        OrderRepository = new GenericRepository<Order>(dbContext);
        ProductRepository = new GenericRepository<Product>(dbContext);
    }

    public void Save()
    {
        dbContext.SaveChanges();
    }

    public IGenericRepository<Customer> CustomerRepository { get; }
    public IGenericRepository<Order> OrderRepository { get; }
    public IGenericRepository<Product> ProductRepository { get; }
}