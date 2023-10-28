using Microsoft.EntityFrameworkCore;

namespace Vk.Data.Context;

public class VkDbContext : DbContext
{
    public VkDbContext(DbContextOptions<VkDbContext> options) : base(options)
    {

    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
    }

}