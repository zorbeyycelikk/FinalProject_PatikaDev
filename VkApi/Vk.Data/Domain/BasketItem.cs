using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vk.Base;

namespace Vk.Data.Domain;


[Table("BasketItem", Schema = "dbo")]
public class BasketItem : BaseModel
{
    public string BasketNumber { get; set; }
    public virtual Basket Basket { get; set; }
    
    public string ProductNumber { get; set; }
    public virtual Product Product { get; set; }
}

class BasketItemConfigruration : IEntityTypeConfiguration<BasketItem>
{
    public void Configure(EntityTypeBuilder<BasketItem> builder)
    {
        builder.Property(x => x.InsertDate).IsRequired();
        builder.Property(x => x.UpdateDate).IsRequired(false);
        builder.Property(x => x.IsActive).IsRequired().HasDefaultValue(true);

        builder.Property(x => x.BasketNumber).IsRequired();
        builder.Property(x => x.ProductNumber).IsRequired();
    }
}