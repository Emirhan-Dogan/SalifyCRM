using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SalifyCRM.OrderManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SalifyCRM.OrderManagement.Persistence.EntityFrameworkCore.Configurations
{
    public class OrderProductDetailConfigurations : IEntityTypeConfiguration<OrderProductDetail>
    {
        public void Configure(EntityTypeBuilder<OrderProductDetail> builder)
        {
            builder.ToTable("order_product_details");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName("id");
            builder.Property(x => x.OrderProductId).HasColumnName("order_product_id");
            builder.Property(x => x.Name).HasColumnName("name");
            builder.Property(x => x.Value).HasColumnName("value");
            builder.Property(x => x.IsDeleted).HasColumnName("is_deleted");
            builder.Property(x => x.CreatedUserId).HasColumnName("created_user_id");
            builder.Property(x => x.CreatedDate).HasColumnName("created_date");
            builder.Property(x => x.Status).HasColumnName("status");
            builder.Property(x => x.LastUpdatedUserId).HasColumnName("last_updated_user_id");
            builder.Property(x => x.LastUpdatedDate).HasColumnName("last_updated_date");
        }
    }
}
