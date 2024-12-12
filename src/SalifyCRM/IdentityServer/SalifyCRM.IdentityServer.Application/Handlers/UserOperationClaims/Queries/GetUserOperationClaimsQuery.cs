using Core.Utilities.Results;
using MediatR;
using SalifyCRM.IdentityServer.Application.RepositoryInterfaces;
using SalifyCRM.IdentityServer.Application.Responses.UserOperationClaims;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalifyCRM.IdentityServer.Application.Handlers.UserOperationClaims.Queries
{
    public class GetUserOperationClaimsQuery : IRequest<IDataResult<List<UserOperationClaimResponse>>>
    {
        public class GetUserOperationClaimsQueryHandler : IRequestHandler<GetUserOperationClaimsQuery, IDataResult<List<UserOperationClaimResponse>>>
        {
            private readonly IUserOperationClaimRepository _userOperationClaimRepository;
            private readonly IMediator _mediator;

            public GetUserOperationClaimsQueryHandler(IUserOperationClaimRepository userOperationClaimRepository, IMediator mediator)
            {
                this._userOperationClaimRepository = userOperationClaimRepository;
                _mediator = mediator;
            }

            public async Task<IDataResult<List<UserOperationClaimResponse>>> Handle(GetUserOperationClaimsQuery request, CancellationToken cancellationToken)
            {
                var userOperationClaims = await _userOperationClaimRepository.GetListAsync(x => x.IsDeleted == false);

                List<UserOperationClaimResponse> result = new List<UserOperationClaimResponse>();
                foreach (var userOperationClaim in userOperationClaims)
                {
                    result.Add(new UserOperationClaimResponse()
                    {
                        Id = userOperationClaim.Id,
                        OperationClaimId = userOperationClaim.OperationClaimId,
                        Status = userOperationClaim.Status,
                        UserId = userOperationClaim.UserId
                    });
                }

                return new SuccessDataResult<List<UserOperationClaimResponse>>(result.ToList());
            }
        }
    }
}
