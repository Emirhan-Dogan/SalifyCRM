using SalifyCRM.IdentityServer.Application.Responses.Groups;
using SalifyCRM.IdentityServer.Application.Responses.Users;
using SalifyCRM.IdentityServer.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalifyCRM.IdentityServer.Application.Responses.OperationClaims
{
    public class OperationClaimDetailResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Descriptions { get; set; }

        public List<UserResponse>? Users { get; set; }
        public List<GroupResponse>? Groups { get; set; }
    }
}
