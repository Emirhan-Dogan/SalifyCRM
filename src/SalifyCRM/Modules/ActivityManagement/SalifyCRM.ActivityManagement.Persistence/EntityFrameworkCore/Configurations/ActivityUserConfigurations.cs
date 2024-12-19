using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SalifyCRM.ActivityManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalifyCRM.ActivityManagement.Persistence.EntityFrameworkCore.Configurations
{
    public class ActivityUserConfigurations : IEntityTypeConfiguration<ActivityUser>
    {
        public void Configure(EntityTypeBuilder<ActivityUser> builder)
        {
            builder.ToTable("activity_users");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName("id");
            builder.Property(x => x.Email).HasColumnName("email");
            builder.Property(x => x.ActivityId).HasColumnName("activity_id");
            builder.Property(x => x.UserId).HasColumnName("user_id");
            builder.Property(x => x.IsVerifyParticipation).HasColumnName("is_verify_participation");
            builder.Property(x => x.ActivityParticipationDate).HasColumnName("activity_participation_date");
            builder.Property(x => x.IsDeleted).HasColumnName("is_deleted");
            builder.Property(x => x.CreatedUserId).HasColumnName("created_user_id");
            builder.Property(x => x.CreatedDate).HasColumnName("created_date");
            builder.Property(x => x.Status).HasColumnName("status");
            builder.Property(x => x.LastUpdatedUserId).HasColumnName("last_updated_user_id");
            builder.Property(x => x.LastUpdatedDate).HasColumnName("last_updated_date");
        }
    }
}
