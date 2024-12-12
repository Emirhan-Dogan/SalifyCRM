using Core.Utilities.Results;
using MediatR;
using SalifyCRM.IdentityServer.Application.RepositoryInterfaces;
using SalifyCRM.IdentityServer.Application.Responses.Groups;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalifyCRM.IdentityServer.Application.Handlers.Groups.Queries
{
    public class GetGroupsQuery : IRequest<IDataResult<List<GroupResponse>>>
    {
        public class GetGroupsQueryHandler : IRequestHandler<GetGroupsQuery, IDataResult<List<GroupResponse>>>
        {
            private readonly IGroupRepository _groupRepository;
            private readonly IMediator _mediator;

            public GetGroupsQueryHandler(IGroupRepository groupRepository, IMediator mediator)
            {
                this._groupRepository = groupRepository;
                this._mediator = mediator;
            }

            public async Task<IDataResult<List<GroupResponse>>> Handle(GetGroupsQuery request, CancellationToken cancellationToken)
            {
                var groups = await _groupRepository.GetListAsync(x => x.IsDeleted == false);
                List<GroupResponse> result = new List<GroupResponse>();
                foreach (var group in groups)
                {
                    result.Add(new GroupResponse()
                    {
                        Descriptions = group.Descriptions,
                        Id = group.Id,
                        Name = group.Name,
                        Status = group.Status
                    });
                }

                return new SuccessDataResult<List<GroupResponse>>(result.ToList());
            }
        }
    }
}
