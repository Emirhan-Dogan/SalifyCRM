using Core.Utilities.Results;
using MediatR;
using SalifyCRM.IdentityServer.Application.Handlers.GroupClaims.ValidationRules;
using SalifyCRM.IdentityServer.Application.RepositoryInterfaces;
using SalifyCRM.IdentityServer.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalifyCRM.IdentityServer.Application.Handlers.GroupClaims.Commands
{
    public class CreateGroupClaimCommand : IRequest<IResult>
    {
        public int CreatedUserId { get; set; }

        public int OperationClaimId { get; set; }
        public int GroupId { get; set; }
        public bool Status { get; set; }

        public class CreateGroupClaimCommandHandler : IRequestHandler<CreateGroupClaimCommand, IResult>
        {
            private readonly IGroupClaimRepository _groupClaimRepository;
            private readonly IMediator _mediator;

            public CreateGroupClaimCommandHandler(IGroupClaimRepository groupClaimRepository, IMediator mediator)
            {
                this._groupClaimRepository = groupClaimRepository;
                this._mediator = mediator;
            }

            public async Task<IResult> Handle(CreateGroupClaimCommand request, CancellationToken cancellationToken)
            {
                var validator = new CreateGroupClaimValidator();
                var result = validator.Validate(request);

                if (!result.IsValid)
                {
                    string errorMessage = "";
                    foreach (var error in result.Errors)
                    {
                        errorMessage += $"Hata: {error.PropertyName} - {error.ErrorMessage}";
                    }
                    return new ErrorResult(errorMessage);
                }

                GroupClaim groupClaimRecord = await _groupClaimRepository.GetAsync(O => O.GroupId == request.GroupId && O.OperationClaimId == request.OperationClaimId);

                if (groupClaimRecord != null)
                {
                    groupClaimRecord.Status = request.Status;
                    groupClaimRecord.IsDeleted = false;
                    groupClaimRecord.LastUpdatedDate = DateTime.UtcNow;
                    groupClaimRecord.LastUpdatedUserId = request.CreatedUserId;

                    _groupClaimRepository.Update(groupClaimRecord);
                    _groupClaimRepository.SaveChanges();

                    return new SuccessResult();
                }

                GroupClaim groupClaim = new GroupClaim()
                {
                    GroupId = request.GroupId,
                    Status = request.Status,
                    OperationClaimId = request.OperationClaimId,
                    CreatedDate = DateTime.UtcNow,
                    LastUpdatedDate = DateTime.UtcNow,
                    IsDeleted = false,
                    LastUpdatedUserId = request.CreatedUserId,
                    CreatedUserId = request.CreatedUserId,
                };

                _groupClaimRepository.Update(groupClaim);
                _groupClaimRepository.SaveChanges();

                return new SuccessResult();
            }
        }
    }
}
