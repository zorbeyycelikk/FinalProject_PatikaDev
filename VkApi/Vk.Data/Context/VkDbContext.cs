using Microsoft.EntityFrameworkCore;
using Vk.Data.Domain;

namespace Vk.Data.Context;

public class VkDbContext : DbContext
{
    public VkDbContext(DbContextOptions<VkDbContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new CustomerConfigruration());
        modelBuilder.ApplyConfiguration(new OrderConfigruration());
        modelBuilder.ApplyConfiguration(new ProductConfigruration());
        modelBuilder.ApplyConfiguration(new AccountConfigruration());
        modelBuilder.ApplyConfiguration(new CardConfigruration());
        modelBuilder.ApplyConfiguration(new BasketConfigruration());
        modelBuilder.ApplyConfiguration(new BasketItemConfigruration());
        modelBuilder.ApplyConfiguration(new CardTransactionConfigruration());
        modelBuilder.ApplyConfiguration(new AccountTransactionConfigruration());
        modelBuilder.ApplyConfiguration(new OpenAccountTransactionConfigruration());
        
        base.OnModelCreating(modelBuilder);
    }
}