using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SalifyCRM.IdentityServer.Domain.Entities;

namespace SalifyCRM.IdentityServer.Persistence.EntityFrameworkCore.Configurations
{
    public class GroupConfigurations : IEntityTypeConfiguration<Group>
    {
        public void Configure(EntityTypeBuilder<Group> builder)
        {
            builder.ToTable("groups");

            builder.HasKey(g => g.Id);
            builder.Property(g => g.Id).HasColumnName("id");
            builder.Property(g => g.Name).HasColumnName("name");
            builder.Property(g => g.Descriptions).HasColumnName("description");
            builder.Property(g => g.IsDeleted).HasColumnName("is_deleted");
            builder.Property(g => g.CreatedUserId).HasColumnName("created_user_id");
            builder.Property(g => g.CreatedDate).HasColumnName("created_date");
            builder.Property(g => g.Status).HasColumnName("status");
            builder.Property(g => g.LastUpdatedUserId).HasColumnName("last_updated_user_id");
            builder.Property(g => g.LastUpdatedDate).HasColumnName("last_updated_date");
        }
    }
}
