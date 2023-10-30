using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vk.Base;


namespace Vk.Data.Domain;

[Table("Order", Schema = "dbo")]
public class Order : BaseModel
{
    public string   OrderNumber      { get; set; }
    public string   Description      { get; set; }
    public string   Address          { get; set; }
    public string   Status           { get; set; }
    
    public string   CustomerNumber   { get; set; }
    public Customer Customer         { get; set; }
    

    public virtual ICollection<OrderProduct> OrderProducts { get; set; }

}

class OrderConfigruration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.Property(x => x.InsertDate).IsRequired();
        builder.Property(x => x.UpdateDate).IsRequired(false);
        builder.Property(x => x.IsActive).IsRequired().HasDefaultValue(true);
        
        builder.Property(x => x.OrderNumber).IsRequired().HasMaxLength(50);
        builder.Property(x => x.Description).IsRequired().HasMaxLength(50);
        builder.Property(x => x.Address).IsRequired().HasMaxLength(50);
        builder.Property(x => x.Address).IsRequired().HasMaxLength(50);
        builder.Property(x => x.CustomerNumber).IsRequired(true);
        
          
        builder.HasMany(o => o.OrderProducts)
            .WithOne(op => op.Order)
            .HasForeignKey(op => op.OrderNumber )
            .HasPrincipalKey(o => o.OrderNumber)
            .IsRequired(true);
    }
}