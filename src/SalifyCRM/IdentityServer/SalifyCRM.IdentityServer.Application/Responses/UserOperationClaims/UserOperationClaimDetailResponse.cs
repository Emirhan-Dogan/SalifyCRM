using SalifyCRM.IdentityServer.Application.Responses.OperationClaims;
using SalifyCRM.IdentityServer.Application.Responses.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalifyCRM.IdentityServer.Application.Responses.UserOperationClaims
{
    public class UserOperationClaimDetailResponse
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int OperationClaimId { get; set; }
        public bool Status { get; set; }

        public UserResponse User { get; set; }
        public OperationClaimResponse OperationClaim { get; set; }
    }
}
