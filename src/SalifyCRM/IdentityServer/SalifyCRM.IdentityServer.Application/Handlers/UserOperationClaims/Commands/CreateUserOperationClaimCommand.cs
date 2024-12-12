using Core.Utilities.Results;
using MediatR;
using SalifyCRM.IdentityServer.Application.Handlers.Groups.ValidationRules;
using SalifyCRM.IdentityServer.Application.Handlers.UserOperationClaims.ValidationRules;
using SalifyCRM.IdentityServer.Application.RepositoryInterfaces;
using SalifyCRM.IdentityServer.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalifyCRM.IdentityServer.Application.Handlers.UserOperationClaims.Commands
{
    public class CreateUserOperationClaimCommand : IRequest<IResult>
    {
        public int CreatedUserId { get; set; }

        public int UserId { get; set; }
        public int OperationClaimId { get; set; }
        public bool Status { get; set; }

        public class CreateUserOperationClaimCommandHandler : IRequestHandler<CreateUserOperationClaimCommand, IResult>
        {
            private readonly IUserOperationClaimRepository _userOperationClaimRepository;
            private readonly IMediator _mediator;

            public CreateUserOperationClaimCommandHandler(
                IUserOperationClaimRepository userOperationClaimRepository,
                IMediator _mediator)
            {
                this._userOperationClaimRepository = userOperationClaimRepository;
                this._mediator = _mediator;
            }

            public async Task<IResult> Handle(CreateUserOperationClaimCommand request, CancellationToken cancellationToken)
            {
                var validator = new CreateUserOperationClaimValidator();
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

                UserOperationClaim userOperationClaimRecord =
                    await _userOperationClaimRepository.GetAsync(O => O.UserId == request.UserId && O.OperationClaimId == request.OperationClaimId);

                if (userOperationClaimRecord != null)
                {
                    userOperationClaimRecord.Status = request.Status;
                    userOperationClaimRecord.IsDeleted = false;

                    userOperationClaimRecord.LastUpdatedDate = DateTime.UtcNow;
                    userOperationClaimRecord.LastUpdatedUserId = request.CreatedUserId;


                    _userOperationClaimRepository.Update(userOperationClaimRecord);
                    _userOperationClaimRepository.SaveChanges();

                    return new SuccessResult();
                }

                UserOperationClaim userOperationClaim = new UserOperationClaim()
                {
                    UserId = request.UserId,
                    OperationClaimId = request.OperationClaimId,
                    Status = request.Status,
                    CreatedDate = DateTime.UtcNow,
                    IsDeleted = false,
                    LastUpdatedDate = DateTime.UtcNow,
                    CreatedUserId = request.CreatedUserId,
                    LastUpdatedUserId = request.CreatedUserId
                };

                _userOperationClaimRepository.Add(userOperationClaim);
                _userOperationClaimRepository.SaveChanges();

                return new SuccessResult();
            }
        }
    }
}
