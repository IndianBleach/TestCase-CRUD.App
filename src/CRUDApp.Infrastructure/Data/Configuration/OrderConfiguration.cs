using CRUDApp.Core.Entities.OrderEntity;
using CRUDApp.Core.Entities.OrderItemEntity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDApp.Infrastructure.Data.Configuration
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasMany(x => x.OrderItems)
                .WithOne(x => x.Order)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Provider)
                .WithMany(x => x.Orders)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
