using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vk.Base;

namespace Vk.Data.Domain;

[Table("Account", Schema = "dbo")]
public class Account : BaseModel
{
    public string CustomerId { get; set; }
    public virtual Customer Customer { get; set; }
    
    public string Name { get; set; }
    public string AccountNumber { get; set; } 
    public string IBAN { get; set; }
    public decimal Balance { get; set; }
    public DateTime OpenDate { get; set; }
    public DateTime? CloseDate { get; set; }

    public string? CardId { get; set; }
    public virtual Card Card { get; set; }
    public virtual ICollection<AccountTransaction> AccountTransactions { get; set; }
    
}

public class AccountConfigruration : IEntityTypeConfiguration<Account>
{
    public void Configure(EntityTypeBuilder<Account> builder)
    {
        builder.Property(x => x.InsertDate).IsRequired();
        builder.Property(x => x.UpdateDate).IsRequired(false);
        builder.Property(x => x.IsActive).IsRequired().HasDefaultValue(true);

        builder.Property(x => x.Name).IsRequired().HasMaxLength(50);
        builder.Property(x => x.AccountNumber).IsRequired(true);
        builder.Property(x => x.IBAN).IsRequired().HasMaxLength(34);
        builder.Property(x => x.Balance).IsRequired().HasPrecision(18,2).HasDefaultValue(0);
        builder.Property(x => x.OpenDate).IsRequired();
        builder.Property(x => x.CloseDate).IsRequired(false);
        
        builder.HasIndex(x => x.AccountNumber).IsUnique(true);
        
        builder.HasOne(x=> x.Card)
            .WithOne(x=> x.Account)
            .HasForeignKey<Card>(c => c.AccountId)
            .HasPrincipalKey<Account>(a => a.Id)
            .IsRequired(true);  
        
        builder.HasMany(c => c.AccountTransactions)
            .WithOne(a => a.Account)
            .HasForeignKey(a => a.AccountId)
            .IsRequired(true);
    }
}

