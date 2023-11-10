using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vk.Base;

namespace Vk.Data.Domain;

[Table("Notice", Schema = "dbo")]
public class Notice : BaseModel
{
    public string ReceiverId { get; set; }
    public string Content { get; set; }
    public bool ReadStatus { get; set; }
    
    public virtual Customer Customer { get; set; }
}

class NoticeConfigruration : IEntityTypeConfiguration<Notice>
{
    public void Configure(EntityTypeBuilder<Notice> builder)
    {
        builder.Property(x => x.InsertDate).IsRequired();
        builder.Property(x => x.UpdateDate).IsRequired(false);
        builder.Property(x => x.IsActive).IsRequired().HasDefaultValue(true);
        
        builder.Property(x => x.Id).IsRequired().HasMaxLength(50);
        builder.Property(x => x.ReceiverId).IsRequired().HasMaxLength(50);
        builder.Property(x => x.Content).IsRequired().HasMaxLength(50);
        builder.Property(x => x.ReadStatus).IsRequired().HasDefaultValue(false);
    }
}