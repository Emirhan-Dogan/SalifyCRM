using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalifyCRM.Common.TokenValidation
{
    public class TokenValidationRequest
    {
        public string Token { get; set; }
        public string RequiredRole { get; set; }
    }
}
