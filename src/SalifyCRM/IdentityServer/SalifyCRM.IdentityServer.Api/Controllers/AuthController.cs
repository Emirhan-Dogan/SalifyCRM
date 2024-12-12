using Core.Utilities.Results;
using Core.Utilities.Security.Hashing;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SalifyCRM.Common.TokenValidation;
using SalifyCRM.IdentityServer.Application;
using SalifyCRM.IdentityServer.Application.DTOs;
using SalifyCRM.IdentityServer.Application.Handlers.Authorizations.Queries;
using SalifyCRM.IdentityServer.Application.RepositoryInterfaces;
using SalifyCRM.IdentityServer.Application.Responses.Authorizations;
using SalifyCRM.IdentityServer.Domain;
using SalifyCRM.IdentityServer.Domain.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace SalifyCRM.IdentityServer.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;
        private IUserRepository _userRepository;
        private ITokenHelper _tokenHelper;

        public AuthController(IMediator mediator, IUserRepository userRepository, ITokenHelper tokenHelper)
        {
            this._mediator = mediator;
            this._userRepository = userRepository;
            _tokenHelper = tokenHelper;
        }

        [HttpPost("personal-login")]
        public async Task<IDataResult<LoginWithUserResponse>> Login(LoginWithUserQuery loginWithUserQuery)
        {
            
            // Asenkron çağrıyı bekleyin
            var result = await _mediator.Send(loginWithUserQuery);

            // Doğrudan IDataResult döndürmelisiniz, OkObjectResult değil
            return result; // SuccessDataResult veya ErrorDataResult dönecektir
        }

        [HttpPost("personal-refresh-token")]
        public IActionResult LoginWithRefreshTokenForPersonal([FromBody] string refreshToken)
        {
            string email = _tokenHelper.ValidateRefreshToken(refreshToken);
            if (String.IsNullOrEmpty(email))
            {
                new ErrorDataResult<object>("Geçersiz refresh token");
            }

            User user = _userRepository.Get(O => O.Email == email);
            var roles = _userRepository.GetClaimsForUser(user.Id);
            // JWT Token oluşturuluyor
            JwtToken jwtToken = new JwtToken
            {
                AccessToken = this._tokenHelper.GenerateAccessToken(user.Email, roles),
                RefreshToken = this._tokenHelper.GenerateRefreshToken(user.Email)
            };
            return Ok(jwtToken);
        }

        [HttpPost("validate-token")]
        public IActionResult ValidateToken([FromBody] TokenValidationRequest request)
        {
            // 1. Normal token doğrulama işlemi
            var claimsPrincipal = _tokenHelper.ValidateAccessToken(request.Token);
            if (claimsPrincipal == null)
                return Unauthorized(new { Message = "Token geçersiz veya kullanıcı giriş yapmamış." });

            // 2. Süresi dolmuş token kontrolü
            var expiryClaim = claimsPrincipal.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Exp);
            if (expiryClaim != null && DateTime.UtcNow > DateTimeOffset.FromUnixTimeSeconds(long.Parse(expiryClaim.Value)).UtcDateTime)
            {
                return StatusCode(498, new { Message = "Token süresi dolmuş." });
            }

            // 3. Token geçerli
            return Ok(new { IsValid = true, Message = "Token geçerli." });
        }


    }
}
