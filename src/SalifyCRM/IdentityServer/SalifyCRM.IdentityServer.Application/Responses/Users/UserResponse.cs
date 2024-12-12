using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalifyCRM.IdentityServer.Application.Responses.Users
{
    public class UserResponse
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
        public bool Status { get; set; }
    }
}
