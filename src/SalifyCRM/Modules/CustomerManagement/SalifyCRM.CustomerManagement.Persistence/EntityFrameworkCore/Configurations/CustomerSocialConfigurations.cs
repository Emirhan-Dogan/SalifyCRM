﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SalifyCRM.CustomerManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalifyCRM.CustomerManagement.Persistence.EntityFrameworkCore.Configurations
{
    public class CustomerSocialConfigurations : IEntityTypeConfiguration<CustomerSocial>
    {
        public void Configure(EntityTypeBuilder<CustomerSocial> builder)
        {
            builder.ToTable("customer_socials");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName("id");
            builder.Property(x => x.CustomerId).HasColumnName("customer_id");
            builder.Property(x => x.SocialMediaId).HasColumnName("social_media_id");
            builder.Property(x => x.SocialMediaLink).HasColumnName("social_media_link");

            builder.Property(x => x.IsDeleted).HasColumnName("is_deleted");
            builder.Property(x => x.Status).HasColumnName("status");
            builder.Property(x => x.CreatedDate).HasColumnName("created_date");
            builder.Property(x => x.CreatedUserId).HasColumnName("created_user_id");
            builder.Property(x => x.LastUpdatedDate).HasColumnName("last_updated_date");
            builder.Property(x => x.LastUpdatedUserId).HasColumnName("last_updated_user_id");
        }
    }
}
