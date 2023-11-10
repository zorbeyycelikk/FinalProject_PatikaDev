using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vk.Base;

namespace Vk.Data.Domain;

[Table("OpenAccountTransaction", Schema = "dbo")]
public class OpenAccountTransaction : BaseModel
{
    public string CustomerId  { get; set; } // SenderId
    public virtual Customer Customer { get; set; }
    
    public string ReceiverCustomerId { get; set; }
    public string refNumber { get; set; } // otomatik oluşacak
    public string PaymentMethod { get; set; } // 
    public string Who { get; set; }
    public decimal Amount { get; set; } // İşlemin Tutarı
    public string Status { get; set; } // Bekleniyor | Basarili | Basarisiz
    public DateTime TransactionDate { get; set; } // islem sonucuna göre olusacak
}

public class OpenAccountTransactionConfigruration : IEntityTypeConfiguration<OpenAccountTransaction>
{
    public void Configure(EntityTypeBuilder<OpenAccountTransaction> builder)
    {
        builder.Property(x => x.InsertDate).IsRequired();
        builder.Property(x => x.UpdateDate).IsRequired(false);
        builder.Property(x => x.IsActive).IsRequired().HasDefaultValue(true);

        builder.Property(x => x.refNumber).IsRequired();
        builder.Property(x => x.Amount).IsRequired().HasPrecision(18, 2);
        builder.HasIndex(x => x.refNumber);
    }
}