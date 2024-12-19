using Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalifyCRM.ProductManagement.Domain.Entities
{
    public class Store : IEntity
    {
        public int Id { get; set; }
        public int AddressId { get; set; }
        public int StoreTypeId { get; set; }
        public string Name { get; set; }
        public string StoreCode { get; set; }
        public int? Capacity { get; set; }
        public string Descriptions { get; set; }
        public bool IsDeleted { get; set; }
        public int CreatedUserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Status { get; set; }
        public int LastUpdatedUserId { get; set; }
        public DateTime LastUpdatedDate { get; set; }

        public StoreType? StoreType { get; set; }
        public Address? Address { get; set; }
    }
}
