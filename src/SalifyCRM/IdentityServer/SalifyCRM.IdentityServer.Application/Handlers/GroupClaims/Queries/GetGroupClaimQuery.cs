using Core.Utilities.Results;
using MediatR;
using SalifyCRM.IdentityServer.Application.RepositoryInterfaces;
using SalifyCRM.IdentityServer.Application.Responses.GroupClaims;
using SalifyCRM.IdentityServer.Application.Responses.Groups;
using SalifyCRM.IdentityServer.Application.Responses.OperationClaims;
using SalifyCRM.IdentityServer.Application.Responses.UserGroups;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalifyCRM.IdentityServer.Application.Handlers.GroupClaims.Queries
{
    public class GetGroupClaimQuery : IRequest<IDataResult<GroupClaimDetailResponse>>
    {
        public int Id { get; set; }

        public class GetGroupClaimQueryHandler : IRequestHandler<GetGroupClaimQuery, IDataResult<GroupClaimDetailResponse>>
        {
            private readonly IMediator _mediator;
            private readonly IGroupClaimRepository _groupClaimRepository;
            private readonly IGroupRepository _groupRepository;
            private readonly IOperationClaimRepository _operationClaimRepository;

            public GetGroupClaimQueryHandler(
                IGroupClaimRepository groupClaimRepository,
                IGroupRepository groupRepository,
                IOperationClaimRepository operationClaimRepository,
                IMediator mediator)
            {
                this._mediator = mediator;
                this._groupClaimRepository = groupClaimRepository;
                this._groupRepository = groupRepository;
                this._operationClaimRepository = operationClaimRepository;
            }

            public async Task<IDataResult<GroupClaimDetailResponse>> Handle(GetGroupClaimQuery request, CancellationToken cancellationToken)
            {
                if (request.Id == null)
                {
                    return new ErrorDataResult<GroupClaimDetailResponse>("Id değeri null olamaz.");
                }

                if (request.Id < 0)
                {
                    return new ErrorDataResult<GroupClaimDetailResponse>("Id değeri negatif olamaz.");
                }

                var groupClaim = await _groupClaimRepository.GetAsync(O => O.Id == request.Id && O.IsDeleted == false);
                if (groupClaim == null)
                {
                    return new ErrorDataResult<GroupClaimDetailResponse>("Kayıt bulunamadı");
                }

                var group = _groupRepository.Get(O => O.Id == groupClaim.GroupId && O.IsDeleted == false);
                var operationClaim = _operationClaimRepository.Get(O => O.Id == groupClaim.OperationClaimId && O.IsDeleted == false);

                GroupClaimDetailResponse groupClaimResponse = new GroupClaimDetailResponse()
                {
                    Id = groupClaim.Id,
                    GroupId = groupClaim.GroupId,
                    OperationClaimId = groupClaim.OperationClaimId,
                    Status = groupClaim.Status,
                    Group = new GroupResponse()
                    {
                        Id = group.Id,
                        Descriptions = group.Descriptions,
                        Name = group.Name,
                        Status = group.Status
                    },
                    OperationClaim = new OperationClaimResponse()
                    {
                        Name = operationClaim.Name,
                        Descriptions = operationClaim.Descriptions,
                        Id = operationClaim.Id
                    }
                };

                return new SuccessDataResult<GroupClaimDetailResponse>(groupClaimResponse);
            }
        }
    }
}
