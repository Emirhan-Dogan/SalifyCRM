using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SalifyCRM.CustomerManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalifyCRM.CustomerManagement.Persistence.EntityFrameworkCore.Configurations
{
    public class AddressConfigurations : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.ToTable("addresses");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName("id");
            builder.Property(x => x.Name).HasColumnName("name");
            builder.Property(x => x.Details).HasColumnName("details");
            builder.Property(x => x.AddressTypeId).HasColumnName("address_type_id");
            builder.Property(x => x.CountryId).HasColumnName("country_id");
            builder.Property(x => x.CityId).HasColumnName("city_id");
            builder.Property(x => x.DistrictId).HasColumnName("district_id");

            builder.Property(x => x.IsDeleted).HasColumnName("is_deleted");
            builder.Property(x => x.Status).HasColumnName("status");
            builder.Property(x => x.CreatedDate).HasColumnName("created_date");
            builder.Property(x => x.CreatedUserId).HasColumnName("created_user_id");
            builder.Property(x => x.LastUpdatedDate).HasColumnName("last_updated_date");
            builder.Property(x => x.LastUpdatedUserId).HasColumnName("last_updated_user_id");
        }
    }
}
