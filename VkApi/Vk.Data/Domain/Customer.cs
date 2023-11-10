using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vk.Base;


namespace Vk.Data.Domain;

[Table("Customer", Schema = "dbo")]
public class Customer : BaseModel
{
    public string Name  { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Role  { get; set; } // admin or bayi  | default value bayi
    public string Password { get; set; }
    public decimal  Profit { get; set; }
    public decimal openAccountLimit { get; set; }
    public virtual ICollection<Account> Accounts { get; set; } 
    public virtual ICollection<Basket> Baskets { get; set; } 
    public virtual ICollection<OpenAccountTransaction> OpenAccountTransactions { get; set; } 

}
class CustomerConfigruration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.Property(x => x.InsertDate).IsRequired();
        builder.Property(x => x.UpdateDate).IsRequired(false);
        builder.Property(x => x.IsActive).IsRequired().HasDefaultValue(true);
        
        builder.Property(x => x.Id).IsRequired().HasMaxLength(50);
        builder.Property(x => x.Name).IsRequired().HasMaxLength(50);
        builder.Property(x => x.Email).IsRequired().HasMaxLength(50);
        builder.Property(x => x.Phone).IsRequired().HasMaxLength(50);
        builder.Property(x => x.Role).IsRequired().HasMaxLength(5).HasDefaultValue("bayi");
        builder.Property(x => x.Password).IsRequired();
        builder.Property(x => x.Profit).IsRequired().HasDefaultValue(0);
        
        builder.HasMany(c => c.Accounts)
            .WithOne(a => a.Customer)
            .HasForeignKey(a => a.CustomerId)
            .HasPrincipalKey(c => c.Id)
            .IsRequired(true);
        
        builder.HasMany(c => c.Baskets)
            .WithOne(a => a.Customer)
            .HasForeignKey(a => a.CustomerId)
            .HasPrincipalKey(c => c.Id)
            .IsRequired(true);
    }
}