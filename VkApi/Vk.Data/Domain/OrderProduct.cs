// using System.ComponentModel.DataAnnotations.Schema;
// using Microsoft.EntityFrameworkCore;
// using Microsoft.EntityFrameworkCore.Metadata.Builders;
// using Vk.Base;
// using Vk.Data.Domain;
//
// namespace Vk.Data;
//
// [Table("OrderProduct", Schema = "dbo")]
// public class OrderProduct : BaseModel
// {
//     public string OrderNumber { get; set; }
//     public virtual Order Order { get; set; }
//
//     public string ProductNumber { get; set; }
//     public virtual Product Product { get; set; }
// }
//
// class OrderProductConfigruration : IEntityTypeConfiguration<OrderProduct>
// {
//     public void Configure(EntityTypeBuilder<OrderProduct> builder)
//     {
//         builder.Property(x => x.InsertDate).IsRequired();
//         builder.Property(x => x.UpdateDate).IsRequired(false);
//         builder.Property(x => x.IsActive).IsRequired().HasDefaultValue(true);
//
//         builder.Property(x => x.OrderNumber).IsRequired();
//         builder.Property(x => x.ProductNumber).IsRequired();
//       
//     }
// }