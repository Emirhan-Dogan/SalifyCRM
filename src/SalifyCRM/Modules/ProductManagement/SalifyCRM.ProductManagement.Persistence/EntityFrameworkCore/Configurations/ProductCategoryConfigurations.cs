﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SalifyCRM.ProductManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SalifyCRM.ProductManagement.Persistence.EntityFrameworkCore.Configurations
{
    public class ProductCategoryConfigurations : IEntityTypeConfiguration<ProductCategory>
    {
        public void Configure(EntityTypeBuilder<ProductCategory> builder)
        {
            builder.ToTable("product_categories");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName("id");
            builder.Property(x => x.ParentCategoryId).HasColumnName("parrent_category_id");
            builder.Property(x => x.Name).HasColumnName("name");
            builder.Property(x => x.Descriptions).HasColumnName("descriptions");
            builder.Property(x => x.IsDeleted).HasColumnName("is_deleted");
            builder.Property(x => x.CreatedUserId).HasColumnName("created_user_id");
            builder.Property(x => x.CreatedDate).HasColumnName("created_date");
            builder.Property(x => x.Status).HasColumnName("status");
            builder.Property(x => x.LastUpdatedUserId).HasColumnName("last_updated_user_id");
            builder.Property(x => x.LastUpdatedDate).HasColumnName("last_updated_date");
        }
    }
}