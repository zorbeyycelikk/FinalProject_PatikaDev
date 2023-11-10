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
        CardTransactionRepository = new GenericRepository<CardTransaction>(dbContext);
        BasketRepository = new GenericRepository<Basket>(dbContext);
        BasketItemRepository = new GenericRepository<BasketItem>(dbContext);
        AccountTransactionRepository = new GenericRepository<AccountTransaction>(dbContext);
        OpenAccountTransactionRepository = new GenericRepository<OpenAccountTransaction>(dbContext);
        NoticeRepository = new GenericRepository<Notice>(dbContext);
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
    public IGenericRepository<CardTransaction> CardTransactionRepository { get; }
    public IGenericRepository<Basket> BasketRepository { get; }
    public IGenericRepository<BasketItem> BasketItemRepository { get; }
    public IGenericRepository<AccountTransaction> AccountTransactionRepository { get; }
    public IGenericRepository<OpenAccountTransaction> OpenAccountTransactionRepository { get; } 
    public IGenericRepository<Notice> NoticeRepository { get; } 

}