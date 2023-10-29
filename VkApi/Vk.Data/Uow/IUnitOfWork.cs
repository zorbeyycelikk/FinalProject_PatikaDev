using Vk.Data.Domain;
using Vk.Data.Repository;

namespace Vk.Data.Uow;

public interface IUnitOfWork
{
    void Save();
    IGenericRepository<Customer> CustomerRepository { get; }
    IGenericRepository<Order> OrderRepository { get; }
    IGenericRepository<Product> ProductRepository { get; }
}