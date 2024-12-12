using Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalifyCRM.CustomerManagement.Domain.Entities
{
    public class CustomerSocial : IEntity
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int SocialMediaId { get; set; }

        public string SocialMediaLink { get; set; }
        public bool IsDeleted { get; set; }
        public string Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedUserId { get; set; }
        public DateTime LastUpdatedDate { get; set; }
        public int LastUpdatedUserId { get; set; }
    }
}
