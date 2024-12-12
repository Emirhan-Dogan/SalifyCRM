using Core.Utilities.Results;
using MediatR;
using SalifyCRM.IdentityServer.Application.RepositoryInterfaces;
using SalifyCRM.IdentityServer.Application.Responses.UserGroups;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalifyCRM.IdentityServer.Application.Handlers.UserGroups.Queries
{
    public class GetUserGroupsQuery : IRequest<IDataResult<List<UserGroupResponse>>>
    {
        public class GetUserGroupsQueryHandler : IRequestHandler<GetUserGroupsQuery, IDataResult<List<UserGroupResponse>>>
        {
            private readonly IUserGroupRepository _userGroupRepository;
            private readonly IMediator _mediator;

            public GetUserGroupsQueryHandler(IUserGroupRepository userGroupRepository, IMediator mediator)
            {
                this._userGroupRepository = userGroupRepository;
                this._mediator = mediator;
            }

            public async Task<IDataResult<List<UserGroupResponse>>> Handle(GetUserGroupsQuery request, CancellationToken cancellationToken)
            {
                var userGroups = await _userGroupRepository.GetListAsync(x => x.IsDeleted == false);

                List<UserGroupResponse> result = new List<UserGroupResponse>();
                foreach (var userGroup in userGroups)
                {
                    result.Add(new UserGroupResponse()
                    {
                        GroupId = userGroup.GroupId,
                        Id = userGroup.Id,
                        Status = userGroup.Status,
                        UserId = userGroup.UserId
                    });
                }

                return new SuccessDataResult<List<UserGroupResponse>>(result.ToList());
            }
        }
    }
}
