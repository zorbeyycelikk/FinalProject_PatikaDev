using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vk.Base;

namespace Vk.Data.Domain;

[Table("Basket", Schema = "dbo")]
public class Basket : BaseModel
{   
    public string CustomerId { get; set; }
    public virtual Customer Customer { get; set; }
    
    public virtual ICollection<BasketItem> BasketItems { get; set; }
    public virtual ICollection<Order> Orders { get; set; }

    
}
class BasketConfigruration : IEntityTypeConfiguration<Basket>
{
    public void Configure(EntityTypeBuilder<Basket> builder)
    {
        builder.Property(x => x.InsertDate).IsRequired();
        builder.Property(x => x.UpdateDate).IsRequired(false);
        builder.Property(x => x.IsActive).IsRequired().HasDefaultValue(true);

        builder.Property(x => x.CustomerId).IsRequired();
        
        builder.HasMany(c => c.BasketItems)
            .WithOne(a => a.Basket)
            .HasForeignKey(a => a.BasketId)
            .HasPrincipalKey(c => c.Id)
            .IsRequired(true);
        
        builder.HasMany(c => c.Orders)
            .WithOne(a => a.Basket)
            .HasForeignKey(a => a.BasketId)
            .HasPrincipalKey(c => c.Id)
            .IsRequired(true);
    }
}