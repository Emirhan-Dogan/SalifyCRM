using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection.Emit;
using SalifyCRM.ActivityManagement.Domain.Entities;

namespace SalifyCRM.ActivityManagement.Persistence.EntityFrameworkCore.Configurations
{
    public class UserConfigurations : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("users");

            builder.HasKey(u => u.Id);
            builder.Property(u => u.Id).HasColumnName("id");
            builder.Property(u => u.FirstName).HasColumnName("first_name");
            builder.Property(u => u.LastName).HasColumnName("last_name");
            builder.Property(u => u.CitizenId).HasColumnName("citizen_id");
            builder.Property(u => u.PhoneNumber).HasColumnName("phone_number");
            builder.Property(u => u.Email).HasColumnName("email");
            builder.Property(u => u.PasswordSalt).HasColumnName("password_salt");
            builder.Property(u => u.PasswordHash).HasColumnName("password_hash");
            builder.Property(u => u.LastLoginDate).HasColumnName("last_login_date");
            builder.Property(u => u.Gender).HasColumnName("gender");
            builder.Property(u => u.BirthDate).HasColumnName("birth_date");
            builder.Property(u => u.ProfileImage).HasColumnName("profile_image");
            builder.Property(u => u.Notes).HasColumnName("notes");
            builder.Property(u => u.IsDeleted).HasColumnName("is_deleted");
            builder.Property(u => u.CreatedUserId).HasColumnName("created_user_id");
            builder.Property(u => u.CreatedDate).HasColumnName("created_date");
            builder.Property(u => u.Status).HasColumnName("status");
            builder.Property(u => u.LastUpdatedUserId).HasColumnName("last_updated_user_id");
            builder.Property(u => u.LastUpdatedDate).HasColumnName("last_updated_date");

        }
    }
}
