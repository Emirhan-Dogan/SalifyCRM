using Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalifyCRM.ProductManagement.Domain.Entities
{
    public class Product : IEntity
    {
        public int Id { get; set; }
        public int StoreId { get; set; }
        public string ProductCode { get; set; }
        public string BarcodeNumber { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int UnitInStock { get; set; }
        public bool IsSubscription { get; set; }
        public decimal Price { get; set; }
        public decimal? Weight { get; set; }
        public decimal? Length { get; set; }
        public decimal? Width { get; set; }
        public decimal? Height { get; set; }
        public decimal? Volume { get; set; }
        public int MinimumStockCount { get; set; }
        public decimal TaxRate { get; set; }
        public bool IsDeleted { get; set; }
        public int CreatedUserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Status { get; set; }
        public int LastUpdatedUserId { get; set; }
        public DateTime LastUpdatedDate { get; set; }

        public Store? Store { get; set; }
    }
}
