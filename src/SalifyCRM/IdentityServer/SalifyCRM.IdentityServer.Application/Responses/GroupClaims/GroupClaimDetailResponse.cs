using SalifyCRM.IdentityServer.Application.Responses.Groups;
using SalifyCRM.IdentityServer.Application.Responses.OperationClaims;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SalifyCRM.IdentityServer.Application.Responses.GroupClaims
{
    public class GroupClaimDetailResponse
    {
        public int Id { get; set; }
        public int OperationClaimId { get; set; }
        public int GroupId { get; set; }
        public bool Status { get; set; }

        public GroupResponse Group { get; set; }
        public OperationClaimResponse OperationClaim { get; set; }
    }
}
