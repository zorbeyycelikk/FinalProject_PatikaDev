using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vk.Base;

namespace Vk.Data.Domain;


[Table("BasketItem", Schema = "dbo")]
public class BasketItem : BaseModel
{
    public int Quantity { get; set; }
    public string BasketId { get; set; }
    public virtual Basket Basket { get; set; }
    
    public string ProductId { get; set; }
    public virtual Product Product { get; set; }
}

class BasketItemConfigruration : IEntityTypeConfiguration<BasketItem>
{
    public void Configure(EntityTypeBuilder<BasketItem> builder)
    {
        builder.Property(x => x.InsertDate).IsRequired();
        builder.Property(x => x.UpdateDate).IsRequired(false);
        builder.Property(x => x.IsActive).IsRequired().HasDefaultValue(true);

        builder.Property(x => x.Quantity).IsRequired();
        builder.Property(x => x.BasketId).IsRequired();
        builder.Property(x => x.ProductId).IsRequired();
    }
}