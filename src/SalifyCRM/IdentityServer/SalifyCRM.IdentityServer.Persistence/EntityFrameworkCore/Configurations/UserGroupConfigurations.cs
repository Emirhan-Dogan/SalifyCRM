using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SalifyCRM.IdentityServer.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalifyCRM.IdentityServer.Persistence.EntityFrameworkCore.Configurations
{
    public class UserGroupConfigurations : IEntityTypeConfiguration<UserGroup>
    {
        public void Configure(EntityTypeBuilder<UserGroup> builder)
        {
            builder.ToTable("user_groups");

            builder.HasKey(ug => ug.Id);
            builder.Property(ug => ug.Id).HasColumnName("id");
            builder.Property(ug => ug.UserId).HasColumnName("user_id");
            builder.Property(ug => ug.GroupId).HasColumnName("group_id");
            builder.Property(ug => ug.IsDeleted).HasColumnName("is_deleted");
            builder.Property(ug => ug.CreatedUserId).HasColumnName("created_user_id");
            builder.Property(ug => ug.CreatedDate).HasColumnName("created_date");
            builder.Property(ug => ug.Status).HasColumnName("status");
            builder.Property(ug => ug.LastUpdatedUserId).HasColumnName("last_updated_user_id");
            builder.Property(ug => ug.LastUpdatedDate).HasColumnName("last_updated_date");
        }
    }
}
