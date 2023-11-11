using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vk.Base;


namespace Vk.Data.Domain;

[Table("Order", Schema = "dbo")]
public class Order : BaseModel
{
    public string   CustomerId      { get; set; }
    public string   OrderNumber      { get; set; }
    public string   Description      { get; set; }
    public string   Address          { get; set; }
    public string   PaymentMethod    { get; set; }
    public string   PaymentRefCode   { get; set; }
    public decimal   Amount          { get; set; }
    public string   Status           { get; set; }
    
    public string BasketId { get; set; }
    public virtual Basket Basket { get; set; }
}

class OrderConfigruration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.Property(x => x.InsertDate).IsRequired();
        builder.Property(x => x.UpdateDate).IsRequired(false);
        builder.Property(x => x.IsActive).IsRequired().HasDefaultValue(true);
        
        builder.Property(x => x.OrderNumber).IsRequired().HasMaxLength(50);
        builder.Property(x => x.Description).HasMaxLength(50);
        builder.Property(x => x.Address).IsRequired().HasMaxLength(50);
        builder.Property(x => x.Amount).IsRequired().HasPrecision(18, 2);

    }
}