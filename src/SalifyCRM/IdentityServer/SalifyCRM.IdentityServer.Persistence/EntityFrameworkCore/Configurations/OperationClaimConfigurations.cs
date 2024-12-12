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
    public class OperationClaimConfigurations : IEntityTypeConfiguration<OperationClaim>
    {
        public void Configure(EntityTypeBuilder<OperationClaim> builder)
        {
            builder.ToTable("operation_claims");

            builder.HasKey(o => o.Id);
            builder.Property(o => o.Id).HasColumnName("id");
            builder.Property(o => o.Name).HasColumnName("name");
            builder.Property(o => o.Descriptions).HasColumnName("description");
            builder.Property(o => o.IsDeleted).HasColumnName("is_deleted");
        }
    }
}
