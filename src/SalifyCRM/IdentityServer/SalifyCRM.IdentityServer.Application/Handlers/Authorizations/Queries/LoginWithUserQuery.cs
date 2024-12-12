using Core.Utilities.Results;
using Core.Utilities.Security.Hashing;
using MediatR;
using SalifyCRM.IdentityServer.Application.DTOs;
using SalifyCRM.IdentityServer.Application.RepositoryInterfaces;
using SalifyCRM.IdentityServer.Application.Responses.Authorizations;
using SalifyCRM.IdentityServer.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SalifyCRM.IdentityServer.Application.Handlers.Authorizations.Queries
{
    public class LoginWithUserQuery : IRequest<IDataResult<LoginWithUserResponse>>
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public class LoginWithUserQueryHandler : IRequestHandler<LoginWithUserQuery, IDataResult<LoginWithUserResponse>>
        {
            private readonly IUserRepository _userRepository;
            private readonly ITokenHelper _tokenHelper;

            public LoginWithUserQueryHandler(IUserRepository userRepository, ITokenHelper tokenHelper)
            {
                this._userRepository = userRepository;
                this._tokenHelper = tokenHelper;
            }

            public async Task<IDataResult<LoginWithUserResponse>> Handle(LoginWithUserQuery request, CancellationToken cancellationToken)
            {
                var user = await _userRepository.GetAsync(O => O.Email == request.Email);

                if (user == null)
                {
                    return new ErrorDataResult<LoginWithUserResponse>("Kullanıcı Bulunmadı");
                }

                if (!HashingHelper.VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
                {
                    return new ErrorDataResult<LoginWithUserResponse>("Şifre Hatalı");
                }
                var roles = _userRepository.GetClaimsForUser(user.Id);

                JwtToken jwtToken = new JwtToken
                {
                    AccessToken = this._tokenHelper.GenerateAccessToken(user.Email, roles),
                    RefreshToken = this._tokenHelper.GenerateRefreshToken(user.Email)
                };

                var data = new LoginWithUserResponse
                {
                    User = user,
                    JwtToken = jwtToken
                };

                return new SuccessDataResult<LoginWithUserResponse>(data);
            }
        }
    }
}
