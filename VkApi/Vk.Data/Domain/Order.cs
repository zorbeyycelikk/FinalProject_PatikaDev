using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vk.Base.Model;

namespace Vk.Data.Domain;

[Table("Order", Schema = "dbo")]
public class Order : BaseModel
{
    public string Description { get; set; }
    public string Address     { get; set; }
    public Guid CustomerId     { get; set; }
    public Customer Customer { get; set; }
    
    public virtual ICollection<Product> Products { get; set; }
}

class OrderConfigruration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.Property(x => x.InsertDate).IsRequired();
        builder.Property(x => x.UpdateDate).IsRequired(false);
        builder.Property(x => x.IsActive).IsRequired().HasDefaultValue(true);
        
        builder.Property(x => x.Id).IsRequired();
        builder.Property(x => x.Description).IsRequired().HasMaxLength(50);
        builder.Property(x => x.Address).IsRequired().HasMaxLength(50);
        
        builder.HasIndex(x => x.CustomerId);
    }
}