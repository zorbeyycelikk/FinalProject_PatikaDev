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
        AccountRepository = new GenericRepository<Account>(dbContext);
        CardRepository = new GenericRepository<Card>(dbContext);

    }

    public void Save()
    {
        dbContext.SaveChanges();
    }

    public IGenericRepository<Customer> CustomerRepository { get; }
    public IGenericRepository<Order> OrderRepository { get; }
    public IGenericRepository<Product> ProductRepository { get; }
    public IGenericRepository<Account> AccountRepository { get; }
    public IGenericRepository<Card> CardRepository { get; }

}