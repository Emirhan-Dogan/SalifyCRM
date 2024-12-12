using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalifyCRM.IdentityServer.Application.Responses.OperationClaims
{
    public class OperationClaimResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Descriptions { get; set; }
    }
}
