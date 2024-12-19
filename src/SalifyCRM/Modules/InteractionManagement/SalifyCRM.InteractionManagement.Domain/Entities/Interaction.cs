using Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalifyCRM.InteractionManagement.Domain.Entities
{
    public class Interaction : IEntity
    {
        public int Id { get; set; }
        public int InteractionTypeId { get; set; }
        public int CustomerId { get; set; }
        public int? OrderId { get; set; }
        public string? OrderCode { get; set; }
        public DateTime InteractionDate { get; set; }
        public string Descriptions { get; set; }
        public bool IsDeleted { get; set; }
        public int CreatedUserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Status { get; set; }
        public int LastUpdatedUserId { get; set; }
        public DateTime LastUpdatedDate { get; set; }

        public Customer? Customer { get; set; }
        public InteractionType? InteractionType { get; set; }
    }

}
