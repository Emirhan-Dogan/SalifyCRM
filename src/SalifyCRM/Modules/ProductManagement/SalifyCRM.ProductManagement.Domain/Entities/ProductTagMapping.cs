using Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalifyCRM.ProductManagement.Domain.Entities
{
    public class ProductTagMapping : IEntity
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int ProductTagId { get; set; }
        public bool IsDeleted { get; set; }
        public int CreatedUserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Status { get; set; }
        public int LastUpdatedUserId { get; set; }
        public DateTime LastUpdatedDate { get; set; }

        public Product? Product { get; set; }
        public ProductTag? ProductTag { get; set; }
    }
}
