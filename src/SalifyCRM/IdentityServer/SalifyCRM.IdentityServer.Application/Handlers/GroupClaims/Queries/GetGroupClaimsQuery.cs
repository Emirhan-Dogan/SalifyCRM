using Core.Utilities.Results;
using MediatR;
using SalifyCRM.IdentityServer.Application.RepositoryInterfaces;
using SalifyCRM.IdentityServer.Application.Responses.GroupClaims;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalifyCRM.IdentityServer.Application.Handlers.GroupClaims.Queries
{
    public class GetGroupClaimsQuery : IRequest<IDataResult<List<GroupClaimResponse>>>
    {
        public class GetGroupClaimsQueryHandler : IRequestHandler<GetGroupClaimsQuery, IDataResult<List<GroupClaimResponse>>>
        {
            private readonly IGroupClaimRepository _groupClaimRepository;
            private readonly IMediator _mediator;

            public GetGroupClaimsQueryHandler(IGroupClaimRepository groupClaimRepository, IMediator mediator)
            {
                this._groupClaimRepository = groupClaimRepository;
                this._mediator = mediator;
            }

            public async Task<IDataResult<List<GroupClaimResponse>>> Handle(GetGroupClaimsQuery request, CancellationToken cancellationToken)
            {
                var groupClaims = await _groupClaimRepository.GetListAsync(x => x.IsDeleted == false);
                List<GroupClaimResponse> result = new List<GroupClaimResponse>();

                foreach (var groupClaim in result)
                {
                    result.Add(new GroupClaimResponse()
                    {
                        GroupId = groupClaim.GroupId,
                        Id = groupClaim.Id,
                        OperationClaimId = groupClaim.OperationClaimId,
                        Status = groupClaim.Status
                    });
                }

                return new SuccessDataResult<List<GroupClaimResponse>>(result.ToList());
            }
        }
    }
}
