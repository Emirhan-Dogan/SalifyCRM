using Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalifyCRM.IdentityServer.Domain.Entities
{
    public class User : IEntity
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? CitizenId { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public DateTime? LastLoginDate { get; set; }
        public string Gender { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? ProfileImage { get; set; }
        public string? Notes { get; set; }

        public string? PasswordSalt { get; set; }
        public string? PasswordHash { get; set; }

        public bool Status { get; set; }
        public bool IsDeleted { get; set; }
        public int CreatedUserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public int LastUpdatedUserId { get; set; }
        public DateTime LastUpdatedDate { get; set; }
    }
}
