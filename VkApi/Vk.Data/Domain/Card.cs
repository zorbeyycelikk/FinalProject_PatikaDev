using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vk.Base;

namespace Vk.Data.Domain;

[Table("Card", Schema = "dbo")]
public class Card : BaseModel
{
    public string AccountId { get; set; }
    public virtual Account Account { get; set; }

    public string CardNumber { get; set; }
    public string Cvv { get; set; } // nnn
    public DateTime ExpiryDate { get; set; } // DDyy
    
    public virtual ICollection<CardTransaction> CardTransactions { get; set; } 

}

public class CardConfigruration : IEntityTypeConfiguration<Card>
{
    public void Configure(EntityTypeBuilder<Card> builder)
    {
        builder.Property(x => x.InsertDate).IsRequired();
        builder.Property(x => x.UpdateDate).IsRequired(false);
        builder.Property(x => x.IsActive).IsRequired().HasDefaultValue(true);

        builder.Property(x => x.AccountId).IsRequired(true);
        builder.Property(x => x.CardNumber).IsRequired();
        builder.Property(x => x.Cvv).IsRequired().HasMaxLength(3);
        builder.Property(x => x.ExpiryDate).IsRequired();

        builder.HasIndex(x => x.AccountId);
        
        builder.HasMany(c => c.CardTransactions)
            .WithOne(a => a.Card)
            .HasForeignKey(a => a.CardId)
            .HasPrincipalKey(c => c.Id)
            .IsRequired(true);
    }
}