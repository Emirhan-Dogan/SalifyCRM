using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SalifyCRM.OrderManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalifyCRM.OrderManagement.Persistence.EntityFrameworkCore.Configurations
{
    public class OrderConfigurations : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("orders");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName("id");
            builder.Property(x => x.OrderStatusId).HasColumnName("order_status_id");
            builder.Property(x => x.CustomerId).HasColumnName("customer_id");
            builder.Property(x => x.CustomerEmail).HasColumnName("customer_email");
            builder.Property(x => x.CustomerPhoneNumber).HasColumnName("customer_phone_number");
            builder.Property(x => x.TotalPrice).HasColumnName("total_price");
            builder.Property(x => x.AddressId).HasColumnName("address_id");
            builder.Property(x => x.AddressCountryId).HasColumnName("address_country_id");
            builder.Property(x => x.AddressCityId).HasColumnName("address_city_id");
            builder.Property(x => x.AddressDistrictId).HasColumnName("address_district_id");
            builder.Property(x => x.AddressDetail).HasColumnName("address_detail");
            builder.Property(x => x.OrderDate).HasColumnName("order_date");
            builder.Property(x => x.IsSubscription).HasColumnName("is_subscription");
            builder.Property(x => x.Notes).HasColumnName("notes");
            builder.Property(x => x.IsDeleted).HasColumnName("is_deleted");
            builder.Property(x => x.CreatedUserId).HasColumnName("created_user_id");
            builder.Property(x => x.CreatedDate).HasColumnName("created_date");
            builder.Property(x => x.Status).HasColumnName("status");
            builder.Property(x => x.LastUpdatedUserId).HasColumnName("last_updated_user_id");
            builder.Property(x => x.LastUpdatedDate).HasColumnName("last_updated_date");
        }
    }
}
