using Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

namespace SalifyCRM.OrderManagement.Domain.Entities
{
    public class Customer : IEntity
    {
        public int Id { get; set; }
        public int CustomerTypeId { get; set; }
        public int OccupationId { get; set; }
        public int MartialStatusId { get; set; }
        public int CustomerCategoryId { get; set; }

        public string CustomerCode { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime BirthDate { get; set; }
        public bool Gender { get; set; }
        public int IncomeLevel { get; set; }
        public DateTime LastLoginDate { get; set; }
        public DateTime LastInteractionDate { get; set; }

        public bool IsDeleted { get; set; }
        public string Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedUserId { get; set; }
        public DateTime LastUpdatedDate { get; set; }
        public int LastUpdatedUserId { get; set; }
    }
}
