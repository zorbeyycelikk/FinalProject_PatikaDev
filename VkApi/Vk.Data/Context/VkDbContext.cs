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

        base.OnModelCreating(modelBuilder);
    }

}