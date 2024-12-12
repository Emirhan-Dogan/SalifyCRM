using Core.Utilities.Results;
using MediatR;
using SalifyCRM.IdentityServer.Application.RepositoryInterfaces;
using SalifyCRM.IdentityServer.Application.Responses.OperationClaims;
using SalifyCRM.IdentityServer.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalifyCRM.IdentityServer.Application.Handlers.OperationClaims.Queries
{
    public class GetOperationClaimsQuery: IRequest<IDataResult<List<OperationClaimResponse>>>
    {

        public class GetOperationClaimsQueryHandler : IRequestHandler<GetOperationClaimsQuery, IDataResult<List<OperationClaimResponse>>>
        {
            private readonly IOperationClaimRepository _operationClaimRepository;
            private readonly IMediator _mediator;

            public GetOperationClaimsQueryHandler(IOperationClaimRepository operationClaimRepository, IMediator mediator)
            {
                this._operationClaimRepository = operationClaimRepository;
                _mediator = mediator;
            }

            public async Task<IDataResult<List<OperationClaimResponse>>> Handle(GetOperationClaimsQuery request, CancellationToken cancellationToken)
            {
                var operationClaims = await _operationClaimRepository.GetListAsync(x => x.IsDeleted == false);
                
                List<OperationClaimResponse> result = new List<OperationClaimResponse>();
                foreach (var operationClaim in operationClaims) 
                {
                    result.Add(new OperationClaimResponse()
                    {
                        Descriptions = operationClaim.Descriptions,
                        Id = operationClaim.Id,
                        Name = operationClaim.Name
                    });
                }

                return new SuccessDataResult<List<OperationClaimResponse>>(result.ToList());
            }
        }
    }
}
