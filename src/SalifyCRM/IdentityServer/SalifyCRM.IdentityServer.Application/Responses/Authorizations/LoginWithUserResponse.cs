using SalifyCRM.IdentityServer.Application.DTOs;
using SalifyCRM.IdentityServer.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalifyCRM.IdentityServer.Application.Responses.Authorizations
{
    public class LoginWithUserResponse
    {
        public User User { get; set; }
        public JwtToken JwtToken { get; set; }
    }
}
