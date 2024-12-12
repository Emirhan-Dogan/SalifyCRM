using Core.Utilities.Results;
using MediatR;
using SalifyCRM.IdentityServer.Application.RepositoryInterfaces;
using SalifyCRM.IdentityServer.Application.Responses.Groups;
using SalifyCRM.IdentityServer.Application.Responses.OperationClaims;
using SalifyCRM.IdentityServer.Application.Responses.UserGroups;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalifyCRM.IdentityServer.Application.Handlers.Groups.Queries
{
    public class GetGroupQuery : IRequest<IDataResult<GroupDetailResponse>>
    {
        public int Id { get; set; }

        public class GetGroupQueryHandler : IRequestHandler<GetGroupQuery, IDataResult<GroupDetailResponse>>
        {
            private readonly IGroupRepository _groupRepository;
            private readonly IUserGroupRepository _userGroupRepository;
            private readonly IGroupClaimRepository _groupClaimRepository;
            private readonly IMediator _mediator;

            public GetGroupQueryHandler(
                IGroupRepository groupRepository,
                IUserGroupRepository userGroupRepository,
                IGroupClaimRepository groupClaimRepository,
                IMediator mediator)
            {
                this._groupRepository = groupRepository;
                this._userGroupRepository = userGroupRepository;
                this._groupClaimRepository = groupClaimRepository;
                this._mediator = mediator;
            }

            public async Task<IDataResult<GroupDetailResponse>> Handle(GetGroupQuery request, CancellationToken cancellationToken)
            {
                if (request.Id == null)
                {
                    return new ErrorDataResult<GroupDetailResponse>("Id değeri null olamaz.");
                }

                if (request.Id < 0)
                {
                    return new ErrorDataResult<GroupDetailResponse>("Id değeri negatif olamaz.");
                }

                var group = await _groupRepository.GetAsync(O => O.Id == request.Id && O.IsDeleted == false);
                if (group == null)
                {
                    return new ErrorDataResult<GroupDetailResponse>("Grup kaydı bulunamadı.");
                }

                var users = _userGroupRepository.GetUsersForGroupId(group.Id);
                var operationClaims = _groupClaimRepository.GetOperationClaimForGroupId(group.Id);

                GroupDetailResponse result = new GroupDetailResponse()
                {
                    Id = group.Id,
                    Name = group.Name,
                    Descriptions = group.Descriptions,
                    Status = group.Status,
                    OperationClaims = operationClaims.ToList(),
                    Users = users.ToList()
                };

                return new SuccessDataResult<GroupDetailResponse>(result);
            }
        }
    }
}
