using Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalifyCRM.OrderManagement.Domain.Entities
{
    public class Order : IEntity
    {
        public int Id { get; set; }
        public int OrderStatusId { get; set; }
        public int CustomerId { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerPhoneNumber { get; set; }
        public decimal TotalPrice { get; set; }
        public int? AddressId { get; set; }
        public int? AddressCountryId { get; set; }
        public int? AddressCityId { get; set; }
        public int? AddressDistrictId { get; set; }
        public string AddressDetail { get; set; }
        public DateTime OrderDate { get; set; }
        public bool IsSubscription { get; set; }
        public string Notes { get; set; }
        public bool IsDeleted { get; set; }
        public int CreatedUserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Status { get; set; }
        public int LastUpdatedUserId { get; set; }
        public DateTime LastUpdatedDate { get; set; }

        public OrderStatus? OrderStatus { get; set; }
        public Customer? Customer { get; set; }
        public Address? Address { get; set; }
    }

}
