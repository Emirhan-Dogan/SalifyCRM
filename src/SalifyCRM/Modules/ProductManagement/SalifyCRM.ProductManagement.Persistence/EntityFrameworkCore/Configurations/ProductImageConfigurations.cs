﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SalifyCRM.ProductManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalifyCRM.ProductManagement.Persistence.EntityFrameworkCore.Configurations
{
    public class ProductImageConfigurations : IEntityTypeConfiguration<ProductImage>
    {
        public void Configure(EntityTypeBuilder<ProductImage> builder)
        {
            builder.ToTable("product_images");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName("id");
            builder.Property(x => x.ProductId).HasColumnName("product_id");
            builder.Property(x => x.ImagePath).HasColumnName("image_path");
            builder.Property(x => x.IsHighlights).HasColumnName("is_highlights");
            builder.Property(x => x.IsDeleted).HasColumnName("is_deleted");
            builder.Property(x => x.CreatedUserId).HasColumnName("created_user_id");
            builder.Property(x => x.CreatedDate).HasColumnName("created_date");
            builder.Property(x => x.Status).HasColumnName("status");
            builder.Property(x => x.LastUpdatedUserId).HasColumnName("last_updated_user_id");
            builder.Property(x => x.LastUpdatedDate).HasColumnName("last_updated_date");
        }
    }
}