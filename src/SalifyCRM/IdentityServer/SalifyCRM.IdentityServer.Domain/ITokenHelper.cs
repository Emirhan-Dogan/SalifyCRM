using SalifyCRM.IdentityServer.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SalifyCRM.IdentityServer.Domain
{
    public interface ITokenHelper
    {
        string GenerateAccessToken(string email, List<OperationClaim> roles);
        string GenerateRefreshToken(string email);
        ClaimsPrincipal ValidateAccessToken(string token);
        string ValidateRefreshToken(string token);
    }

}
