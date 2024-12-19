using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SalifyCRM.InteractionManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SalifyCRM.InteractionManagement.Persistence.EntityFrameworkCore.Configurations
{
    public class CustomerConfigurations : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable("customers");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName("id");

            builder.Property(x => x.CustomerCode).HasColumnName("customer_code");
            builder.Property(x => x.FirstName).HasColumnName("first_name");
            builder.Property(x => x.LastName).HasColumnName("last_name");
            builder.Property(x => x.Email).HasColumnName("email");
            builder.Property(x => x.PhoneNumber).HasColumnName("phone_number");
            builder.Property(x => x.BirthDate).HasColumnName("birth_date");
            builder.Property(x => x.Gender).HasColumnName("gender");
            builder.Property(x => x.IncomeLevel).HasColumnName("income_level");
            builder.Property(x => x.LastLoginDate).HasColumnName("last_login_date");
            builder.Property(x => x.LastInteractionDate).HasColumnName("last_interaction_date");
            builder.Property(x => x.CustomerTypeId).HasColumnName("customer_type_id");
            builder.Property(x => x.OccupationId).HasColumnName("occupation_id");
            builder.Property(x => x.MartialStatusId).HasColumnName("martial_status_id");
            builder.Property(x => x.CustomerCategoryId).HasColumnName("customer_category_id");

            builder.Property(x => x.IsDeleted).HasColumnName("is_deleted");
            builder.Property(x => x.Status).HasColumnName("status");
            builder.Property(x => x.CreatedDate).HasColumnName("created_date");
            builder.Property(x => x.CreatedUserId).HasColumnName("created_user_id");
            builder.Property(x => x.LastUpdatedDate).HasColumnName("last_updated_date");
            builder.Property(x => x.LastUpdatedUserId).HasColumnName("last_updated_user_id");
        }
    }
}
