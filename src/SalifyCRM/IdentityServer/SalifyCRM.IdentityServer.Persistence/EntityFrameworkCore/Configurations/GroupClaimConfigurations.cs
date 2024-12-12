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
    public class GroupClaimConfigurations : IEntityTypeConfiguration<GroupClaim>
    {
        public void Configure(EntityTypeBuilder<GroupClaim> builder)
        {
            builder.ToTable("group_claims");

            builder.HasKey(gc => gc.Id);
            builder.Property(gc => gc.Id).HasColumnName("id");
            builder.Property(gc => gc.GroupId).HasColumnName("group_id");
            builder.Property(gc => gc.OperationClaimId).HasColumnName("operation_claim_id");
            builder.Property(gc => gc.IsDeleted).HasColumnName("is_deleted");
            builder.Property(gc => gc.CreatedUserId).HasColumnName("created_user_id");
            builder.Property(gc => gc.CreatedDate).HasColumnName("created_date");
            builder.Property(gc => gc.Status).HasColumnName("status");
            builder.Property(gc => gc.LastUpdatedUserId).HasColumnName("last_updated_user_id");
            builder.Property(gc => gc.LastUpdatedDate).HasColumnName("last_updated_date");
        }
    }
}
