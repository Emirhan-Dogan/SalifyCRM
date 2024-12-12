using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalifyCRM.IdentityServer.Application.Responses.GroupClaims
{
    public class GroupClaimResponse
    {
        public int Id { get; set; }
        public int OperationClaimId { get; set; }
        public int GroupId { get; set; }
        public bool Status { get; set; }
    }
}
