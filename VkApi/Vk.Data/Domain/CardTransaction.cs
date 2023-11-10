using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vk.Base;

namespace Vk.Data.Domain;

[Table("CardTransaction", Schema = "dbo")]
public class CardTransaction : BaseModel
{
    public string CardId { get; set; }
    public virtual Card Card { get; set; }
    
    public string transactionRefNumber { get; set; } // otomatik yaratilirken olusturulacak
    public string receiverAccountNumber  { get; set; } // Alıcının hesap numarasi

    public string CardNumber { get; set; }
    public string Cvv { get; set; } // nnn
    public DateTime ExpiryDate { get; set; } // DDyy
    
    public decimal Amount { get; set; } // İşlemin Tutarı
    public string Status { get; set; } // Bekleniyor | Basarili | Basarisiz
}

public class CardTransactionConfigruration : IEntityTypeConfiguration<CardTransaction>
{
    public void Configure(EntityTypeBuilder<CardTransaction> builder)
    {
        builder.Property(x => x.InsertDate).IsRequired();
        builder.Property(x => x.UpdateDate).IsRequired(false);
        builder.Property(x => x.IsActive).IsRequired().HasDefaultValue(true);

        builder.Property(x => x.CardId).IsRequired(true);
        builder.Property(x => x.transactionRefNumber).IsRequired(true);
        builder.Property(x => x.receiverAccountNumber).IsRequired(true);
        builder.Property(x => x.CardNumber).IsRequired();
        builder.Property(x => x.Cvv).IsRequired().HasMaxLength(3);
        builder.Property(x => x.ExpiryDate).IsRequired();
        
        builder.Property(x => x.Amount).IsRequired().HasPrecision(18, 2);
        builder.Property(x => x.Status).IsRequired();
        
        builder.HasIndex(x => x.CardId);
    }
    
}