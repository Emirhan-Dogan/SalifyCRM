using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalifyCRM.IdentityServer.Domain
{
    public class TokenOptions
    {
        public string Issuer { get; set; }  // Token'ın yayıncısı
        public string Audience { get; set; }  // Token'ı kullanacak kitle
        public int AccessTokenExpiration { get; set; }  // Access Token'ın geçerlilik süresi (dakika cinsinden)
        public int RefreshTokenExpiration { get; set; }  // Refresh Token'ın geçerlilik süresi (gün cinsinden)
        public string SecurityKey { get; set; }  // Secret key (anahtar)
    }
}
