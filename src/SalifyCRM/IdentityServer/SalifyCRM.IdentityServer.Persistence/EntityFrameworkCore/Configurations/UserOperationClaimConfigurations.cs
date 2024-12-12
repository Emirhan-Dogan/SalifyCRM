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
    public class UserOperationClaimConfigurations : IEntityTypeConfiguration<UserOperationClaim>
    {
        public void Configure(EntityTypeBuilder<UserOperationClaim> builder)
        {
            builder.ToTable("user_operation_claims");

            builder.HasKey(uo => uo.Id);
            builder.Property(uo => uo.Id).HasColumnName("id");
            builder.Property(uo => uo.UserId).HasColumnName("user_id");
            builder.Property(uo => uo.OperationClaimId).HasColumnName("operation_claim_id");
            builder.Property(uo => uo.IsDeleted).HasColumnName("is_deleted");
            builder.Property(uo => uo.CreatedUserId).HasColumnName("created_user_id");
            builder.Property(uo => uo.CreatedDate).HasColumnName("created_date");
            builder.Property(uo => uo.Status).HasColumnName("status");
            builder.Property(uo => uo.LastUpdatedUserId).HasColumnName("last_updated_user_id");
            builder.Property(uo => uo.LastUpdatedDate).HasColumnName("last_updated_date");
        }
    }
}
