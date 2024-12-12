using Core.Utilities.Results;
using MediatR;
using SalifyCRM.IdentityServer.Application.RepositoryInterfaces;
using SalifyCRM.IdentityServer.Application.Responses.UserOperationClaims;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalifyCRM.IdentityServer.Application.Handlers.UserOperationClaims.Commands
{
    public class DeleteUserOperationClaimCommand : IRequest<IResult>
    {
        public int DeletedUserId { get; set; }
        public int Id { get; set; }

        public class DeleteUserOperationClaimCommandHandler : IRequestHandler<DeleteUserOperationClaimCommand, IResult>
        {
            private readonly IUserOperationClaimRepository _userOperationClaimRepository;
            private readonly IMediator _mediator;

            public DeleteUserOperationClaimCommandHandler(IUserOperationClaimRepository userOperationClaimRepository, IMediator mediator)
            {
                this._userOperationClaimRepository = userOperationClaimRepository;
                this._mediator = mediator;
            }

            public async Task<IResult> Handle(DeleteUserOperationClaimCommand request, CancellationToken cancellationToken)
            {
                if (request.Id == null || request.DeletedUserId == null)
                {
                    return new ErrorResult("Değer null olamaz.");
                }

                if (request.Id < 0 || request.DeletedUserId < 0)
                {
                    return new ErrorResult("Değer negatif olamaz.");
                }

                var userOperationClaim = await _userOperationClaimRepository.GetAsync(O => O.Id == request.Id && O.IsDeleted == false);
                if(userOperationClaim == null)
                {
                    return new ErrorResult("Kayıt bulunamadı ya da zaten silinmiş.");
                }

                userOperationClaim.IsDeleted = true;

                userOperationClaim.LastUpdatedDate = DateTime.UtcNow;
                userOperationClaim.LastUpdatedUserId = request.DeletedUserId;

                _userOperationClaimRepository.Update(userOperationClaim);
                _userOperationClaimRepository.SaveChanges();

                return new SuccessResult();
            }
        }
    }
}
