using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vk.Base;

namespace Vk.Data.Domain;

[Table("AccountTransaction", Schema = "dbo")]
public class AccountTransaction : BaseModel
{
    public string AccountId { get; set; }           // SenderAccountId
    public virtual Account Account { get; set; }
    
    public string refNumber { get; set; } // otomatik oluşacak
    public string IBAN { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int Amount { get; set; }
    public string Who { get; set; }
    public string PaymentMethod { get; set; } // For sender -> eft , havale // For receiver eft , havale , card
    public string Status  { get; set; }           // islem sonucuna göre olusacak
    public DateTime TransactionDate { get; set; } // islem sonucuna göre olusacak
}

public class AccountTransactionConfigruration : IEntityTypeConfiguration<AccountTransaction>
{
    public void Configure(EntityTypeBuilder<AccountTransaction> builder)
    {
        builder.Property(x => x.InsertDate).IsRequired();
        builder.Property(x => x.UpdateDate).IsRequired(false);
        builder.Property(x => x.IsActive).IsRequired().HasDefaultValue(true);

        builder.Property(x => x.refNumber).IsRequired();
        builder.Property(x => x.IBAN).IsRequired().HasMaxLength(34);
        builder.Property(x => x.Name).IsRequired().HasPrecision(18,2).HasDefaultValue(0);
        builder.Property(x => x.Description).IsRequired();
        builder.Property(x => x.Amount).IsRequired();

        builder.HasIndex(x => x.refNumber);
    }
}

