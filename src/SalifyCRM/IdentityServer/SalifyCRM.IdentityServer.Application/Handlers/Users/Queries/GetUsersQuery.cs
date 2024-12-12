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
    public class GetUsersQuery : IRequest<IDataResult<List<UserResponse>>>
    {
        public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, IDataResult<List<UserResponse>>>
        {
            private readonly IUserRepository _userRepository;
            private readonly IMediator _mediator;

            public GetUsersQueryHandler(IUserRepository userRepository, IMediator mediator)
            {
                this._userRepository = userRepository;
                this._mediator = mediator;
            }

            public async Task<IDataResult<List<UserResponse>>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
            {
                var users = await _userRepository.GetListAsync(x=>x.IsDeleted==false);

                List<UserResponse> result = new List<UserResponse>();
                foreach (var user in users)
                {
                    result.Add(new UserResponse()
                    {
                        BirthDate = user.BirthDate,
                        CitizenId = user.CitizenId,
                        Email = user.Email,
                        FirstName = user.FirstName,
                        Gender = user.Gender,
                        Id = user.Id,
                        LastLoginDate = user.LastLoginDate,
                        LastName = user.LastName,
                        Notes = user.Notes,
                        PhoneNumber = user.PhoneNumber,
                        ProfileImage = user.ProfileImage,
                        Status = user.Status
                    });
                }

                return new SuccessDataResult<List<UserResponse>>(result.ToList());
            }
        }
    }
}
