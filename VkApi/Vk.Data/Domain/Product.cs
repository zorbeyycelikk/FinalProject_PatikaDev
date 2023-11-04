using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vk.Base;

namespace Vk.Data.Domain;

[Table("Product", Schema = "dbo")]
public class Product : BaseModel
{
    public string Name           { get; set; }
    public string Category       { get; set; }
    public int    Stock          { get; set; }
    public float  Price          { get; set; }
    public string imgUrl         { get; set; }
    
    public virtual ICollection<BasketItem> BasketItems { get; set; }
    
}

class ProductConfigruration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.Property(x => x.InsertDate).IsRequired();
        builder.Property(x => x.UpdateDate).IsRequired(false);
        builder.Property(x => x.IsActive).IsRequired().HasDefaultValue(true);
        
        builder.Property(x => x.Name).IsRequired().HasMaxLength(50);
        builder.Property(x => x.Stock).IsRequired().HasDefaultValue(0);
        builder.Property(x => x.Price).IsRequired().HasDefaultValue(0);
        builder.Property(x => x.Category).IsRequired().HasMaxLength(50);
        
        builder.HasMany(o => o.BasketItems)
            .WithOne(op => op.Product)
            .HasForeignKey(op => op.ProductId )
            .HasPrincipalKey(o => o.Id)
            .IsRequired(true);
    }
}