using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Real_e_commerce.Core.Entities.OrderAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Real_e_commerce.Infrastructure.Config
{
    public class OrderConfigration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.OwnsOne(x => x.ShippingAdress, o => o.WithOwner());
            builder.OwnsOne(x=>x.PaymentSummery ,o => o.WithOwner());
            builder.Property(x => x.Status).HasConversion(
                o => o.ToString(),
                o => (OrderStatus)Enum.Parse(typeof(OrderStatus), o)
                );
            builder.Property(x => x.SubTotal).HasColumnType("decimal(18,2)");
            builder.HasMany(x => x.OrderItems).WithOne().OnDelete(DeleteBehavior.Cascade);
            builder.Property(x => x.OrderDate).HasConversion(
                d=>d.ToUniversalTime(),
                d=>DateTime.SpecifyKind(d,DateTimeKind.Utc)
                );
        }
    }
}
