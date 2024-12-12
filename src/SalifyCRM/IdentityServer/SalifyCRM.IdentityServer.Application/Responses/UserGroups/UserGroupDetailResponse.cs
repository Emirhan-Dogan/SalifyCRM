using SalifyCRM.IdentityServer.Application.Responses.Groups;
using SalifyCRM.IdentityServer.Application.Responses.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalifyCRM.IdentityServer.Application.Responses.UserGroups
{
    public class UserGroupDetailResponse
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int GroupId { get; set; }
        public bool Status { get; set; }

        public UserResponse User { get; set; }
        public GroupResponse Group { get; set; }
    }
}
