using Core.Utilities.Results;
using MediatR;
using SalifyCRM.IdentityServer.Application.RepositoryInterfaces;
using SalifyCRM.IdentityServer.Application.Responses.UserOperationClaims;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalifyCRM.IdentityServer.Application.Handlers.GroupClaims.Commands
{
    public class DeleteGroupClaimCommand : IRequest<IResult>
    {
        public int DeletedUserId { get; set; }
        public int Id { get; set; }

        public class DeleteGroupClaimCommandHandler : IRequestHandler<DeleteGroupClaimCommand, IResult>
        {
            private readonly IGroupClaimRepository _groupClaimRepository;
            private readonly IMediator _mediator;

            public DeleteGroupClaimCommandHandler(IGroupClaimRepository groupClaimRepository, IMediator mediator)
            {
                this._groupClaimRepository = groupClaimRepository;
                this._mediator = mediator;
            }

            public async Task<IResult> Handle(DeleteGroupClaimCommand request, CancellationToken cancellationToken)
            {
                if (request.Id == null || request.DeletedUserId == null)
                {
                    return new ErrorResult("Değer null olamaz.");
                }

                if (request.Id < 0 || request.DeletedUserId < 0)
                {
                    return new ErrorResult("Değer negatif olamaz.");
                }

                var groupClaim = await _groupClaimRepository.GetAsync(O => O.Id == request.Id && O.IsDeleted == false);
                if (groupClaim == null)
                {
                    return new ErrorResult("Kayıt bulunamadı ya da zaten silinmiş.");
                }

                groupClaim.IsDeleted = true;
                groupClaim.LastUpdatedDate = DateTime.UtcNow;
                groupClaim.LastUpdatedUserId = request.DeletedUserId;

                _groupClaimRepository.Update(groupClaim);
                _groupClaimRepository.SaveChanges();

                return new SuccessResult();
            }
        }
    }
}
