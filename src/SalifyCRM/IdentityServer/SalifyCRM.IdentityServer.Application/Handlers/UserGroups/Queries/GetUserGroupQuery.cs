using Core.Utilities.Results;
using MediatR;
using SalifyCRM.IdentityServer.Application.RepositoryInterfaces;
using SalifyCRM.IdentityServer.Application.Responses.UserGroups;
using SalifyCRM.IdentityServer.Application.Responses.UserOperationClaims;
using SalifyCRM.IdentityServer.Application.Responses.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalifyCRM.IdentityServer.Application.Handlers.UserGroups.Queries
{
    public class GetUserGroupQuery : IRequest<IDataResult<UserGroupDetailResponse>>
    {
        public int Id { get; set; }

        public class GetUserGroupQueryHandler : IRequestHandler<GetUserGroupQuery, IDataResult<UserGroupDetailResponse>>
        {
            private readonly IUserGroupRepository _userGroupRepository;
            private readonly IUserRepository _userRepository;
            private readonly IGroupRepository _groupRepository;
            private readonly IMediator _mediator;

            public GetUserGroupQueryHandler(
                IUserGroupRepository userGroupRepository,
                IUserRepository userRepository,
                IGroupRepository groupRepository,
                IMediator mediator)
            {
                this._userGroupRepository = userGroupRepository;
                this._userRepository = userRepository;
                this._groupRepository = groupRepository;
                this._mediator = mediator;
            }

            public async Task<IDataResult<UserGroupDetailResponse>> Handle(GetUserGroupQuery request, CancellationToken cancellationToken)
            {
                if (request.Id == null)
                {
                    return new ErrorDataResult<UserGroupDetailResponse>("Id değeri null olamaz.");
                }

                if (request.Id < 0)
                {
                    return new ErrorDataResult<UserGroupDetailResponse>("Id değeri negatif olamaz.");
                }

                var userGroup = await _userGroupRepository.GetAsync(O => O.Id == request.Id && O.IsDeleted == false);
                if (userGroup == null)
                {
                    return new ErrorDataResult<UserGroupDetailResponse>("Kayıt bulunamadı");
                }

                var user = await _userRepository.GetAsync(O => O.Id == userGroup.UserId && O.IsDeleted == false);
                var group = await _groupRepository.GetAsync(O => O.Id == userGroup.GroupId && O.IsDeleted == false);

                UserGroupDetailResponse result = new UserGroupDetailResponse()
                {
                    Id = userGroup.Id,
                    GroupId = userGroup.Id,
                    UserId = userGroup.UserId,
                    Status = userGroup.Status,
                    User = new UserResponse()
                    {
                        Id = user.Id,
                        Status = user.Status,
                        BirthDate = user.BirthDate,
                        CitizenId = user.CitizenId,
                        Email = user.Email,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Gender = user.Gender,
                        LastLoginDate = user.LastLoginDate,
                        Notes = user.Notes,
                        PhoneNumber = user.PhoneNumber,
                        ProfileImage = user.ProfileImage
                    },
                    Group = new Responses.Groups.GroupResponse()
                    {
                        Id = group.Id,
                        Descriptions = group.Descriptions,
                        Name = group.Name,
                        Status = group.Status
                    }
                };

                return new SuccessDataResult<UserGroupDetailResponse>(result);
            }
        }
    }
}
