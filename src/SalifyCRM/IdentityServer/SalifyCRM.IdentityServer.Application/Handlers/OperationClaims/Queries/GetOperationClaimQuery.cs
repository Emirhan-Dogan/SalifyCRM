using Core.Utilities.Results;
using MediatR;
using SalifyCRM.IdentityServer.Application.RepositoryInterfaces;
using SalifyCRM.IdentityServer.Application.Responses.OperationClaims;
using SalifyCRM.IdentityServer.Application.Responses.UserGroups;
using SalifyCRM.IdentityServer.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalifyCRM.IdentityServer.Application.Handlers.OperationClaims.Queries
{
    public class GetOperationClaimQuery : IRequest<IDataResult<OperationClaimDetailResponse>>
    {
        public int Id { get; set; }

        public class GetOperationClaimQueryHandler : IRequestHandler<GetOperationClaimQuery, IDataResult<OperationClaimDetailResponse>>
        {
            private readonly IOperationClaimRepository _operationClaimRepository;
            private readonly IUserOperationClaimRepository _userOperationClaimRepository;
            private readonly IGroupClaimRepository _groupClaimRepository;
            private readonly IMediator _mediator;

            public GetOperationClaimQueryHandler(
                IOperationClaimRepository operationClaimRepository,
                IUserOperationClaimRepository userOperationClaimRepository,
                IGroupClaimRepository groupClaimRepository,
                IMediator mediator)
            {
                this._operationClaimRepository = operationClaimRepository;
                this._userOperationClaimRepository = userOperationClaimRepository;
                this._groupClaimRepository = groupClaimRepository;
                this._mediator = mediator;
            }

            public async Task<IDataResult<OperationClaimDetailResponse>> Handle(GetOperationClaimQuery request, CancellationToken cancellationToken)
            {
                if (request.Id == null)
                {
                    return new ErrorDataResult<OperationClaimDetailResponse>("Id değeri null olamaz.");
                }

                if (request.Id < 0)
                {
                    return new ErrorDataResult<OperationClaimDetailResponse>("Id değeri negatif olamaz.");
                }

                var operationClaim = await _operationClaimRepository.GetAsync(O => O.Id == request.Id && O.IsDeleted == false);
                if (operationClaim == null)
                {
                    return new ErrorDataResult<OperationClaimDetailResponse>("Yetki kaydı bulunamadı.");
                }

                var groups = _groupClaimRepository.GetGroupForOperationClaimId(request.Id);
                var users = _userOperationClaimRepository.GetUsersForOperationClaimId(request.Id);

                var result = new OperationClaimDetailResponse()
                {
                    Id = operationClaim.Id,
                    Descriptions = operationClaim.Descriptions,
                    Name = operationClaim.Name,
                    Groups = groups.ToList(),
                    Users = users.ToList()
                };

                return new SuccessDataResult<OperationClaimDetailResponse>(result);
            }
        }
    }
}
