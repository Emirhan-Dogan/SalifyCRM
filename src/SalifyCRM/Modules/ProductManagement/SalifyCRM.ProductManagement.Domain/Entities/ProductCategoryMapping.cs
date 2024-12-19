using Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

namespace SalifyCRM.ProductManagement.Domain.Entities
{
    public class ProductCategoryMapping : IEntity
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int ProductCategoryId { get; set; }
        public bool IsDeleted { get; set; }
        public int CreatedUserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Status { get; set; }
        public int LastUpdatedUserId { get; set; }
        public DateTime LastUpdatedDate { get; set; }

        public Product? Product { get; set; }
        public ProductCategory? ProductCategory { get; set; }
    }
}
