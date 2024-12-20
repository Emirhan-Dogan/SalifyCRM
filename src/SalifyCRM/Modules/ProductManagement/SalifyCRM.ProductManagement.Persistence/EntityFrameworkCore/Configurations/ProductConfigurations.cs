using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SalifyCRM.ProductManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml;

namespace SalifyCRM.ProductManagement.Persistence.EntityFrameworkCore.Configurations
{
    public class ProductConfigurations : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("products");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName("id");
            builder.Property(x => x.StoreId).HasColumnName("store_id");
            builder.Property(x => x.ProductCode).HasColumnName("product_code");
            builder.Property(x => x.BarcodeNumber).HasColumnName("barcode_number");
            builder.Property(x => x.Name).HasColumnName("name");
            builder.Property(x => x.Description).HasColumnName("description");
            builder.Property(x => x.UnitInStock).HasColumnName("unit_in_stock");
            builder.Property(x => x.IsSubscription).HasColumnName("is_subscription");
            builder.Property(x => x.Price).HasColumnName("price");
            builder.Property(x => x.Weight).HasColumnName("weight");
            builder.Property(x => x.Length).HasColumnName("length");
            builder.Property(x => x.Width).HasColumnName("width");
            builder.Property(x => x.Height).HasColumnName("height");
            builder.Property(x => x.Volume).HasColumnName("volume");
            builder.Property(x => x.MinimumStockCount).HasColumnName("minimum_stock_count");
            builder.Property(x => x.TaxRate).HasColumnName("tax_rate");
            builder.Property(x => x.IsDeleted).HasColumnName("is_deleted");
            builder.Property(x => x.CreatedUserId).HasColumnName("created_user_id");
            builder.Property(x => x.CreatedDate).HasColumnName("created_date");
            builder.Property(x => x.Status).HasColumnName("status");
            builder.Property(x => x.LastUpdatedUserId).HasColumnName("last_updated_user_id");
            builder.Property(x => x.LastUpdatedDate).HasColumnName("last_updated_date");
        }
    }
}
