using Core.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SalifyCRM.OrderManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalifyCRM.OrderManagement.Persistence.EntityFrameworkCore.Configurations
{
    public class OrderProductConfigurations : IEntityTypeConfiguration<OrderProduct>
    {
        public void Configure(EntityTypeBuilder<OrderProduct> builder)
        {
            builder.ToTable("order_products");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName("id");
            builder.Property(x => x.OrderId).HasColumnName("order_id");
            builder.Property(x => x.ProductId).HasColumnName("product_id");
            builder.Property(x => x.Piece).HasColumnName("piece");
            builder.Property(x => x.UnitInPrice).HasColumnName("unit_in_price");
            builder.Property(x => x.IsDeleted).HasColumnName("is_deleted");
            builder.Property(x => x.CreatedUserId).HasColumnName("created_user_id");
            builder.Property(x => x.CreatedDate).HasColumnName("created_date");
            builder.Property(x => x.Status).HasColumnName("status");
            builder.Property(x => x.LastUpdatedUserId).HasColumnName("last_updated_user_id");
            builder.Property(x => x.LastUpdatedDate).HasColumnName("last_updated_date");
        }
    }
}
