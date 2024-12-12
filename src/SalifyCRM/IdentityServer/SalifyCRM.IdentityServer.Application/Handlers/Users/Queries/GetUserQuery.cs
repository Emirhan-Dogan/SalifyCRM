using Core.Utilities.Results;
using MediatR;
using SalifyCRM.IdentityServer.Application.RepositoryInterfaces;
using SalifyCRM.IdentityServer.Application.Responses.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalifyCRM.IdentityServer.Application.Handlers.Users.Queries
{
    public class GetUserQuery : IRequest<IDataResult<UserDetailResponse>>
    {
        public int Id { get; set; }

        public class GetUserQueryHandler : IRequestHandler<GetUserQuery, IDataResult<UserDetailResponse>>
        {
            private readonly IUserRepository _userRepository;
            private readonly IMediator _mediator;

            public GetUserQueryHandler(IUserRepository userRepository, IMediator mediator)
            {
                this._userRepository = userRepository;
                this._mediator = mediator;
            }

            public async Task<IDataResult<UserDetailResponse>> Handle(GetUserQuery request, CancellationToken cancellationToken)
            {
                if (request.Id == null)
                {
                    return new ErrorDataResult<UserDetailResponse>("Id değeri null olamaz.");
                }

                if (request.Id < 0)
                {
                    return new ErrorDataResult<UserDetailResponse>("Id değeri negatif olamaz.");
                }

                var user = await _userRepository.GetAsync(O => O.Id == request.Id && O.IsDeleted == false);
                if (user == null)
                {
                    return new ErrorDataResult<UserDetailResponse>("Kullanıcı kaydı bulunamadı");
                }

                UserDetailResponse result = new UserDetailResponse()
                {
                    Id = user.Id,
                    BirthDate = user.BirthDate,
                    CitizenId = user.CitizenId,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Gender = user.Gender,
                    LastLoginDate = user.LastLoginDate,
                    Notes = user.Notes,
                    PhoneNumber = user.PhoneNumber,
                    ProfileImage = user.ProfileImage,
                    Status = user.Status
                };

                return new SuccessDataResult<UserDetailResponse>(result);
            }
        }
    }
}
