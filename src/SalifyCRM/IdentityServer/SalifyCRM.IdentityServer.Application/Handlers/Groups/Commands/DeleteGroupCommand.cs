using Core.Utilities.Results;
using MediatR;
using SalifyCRM.IdentityServer.Application.RepositoryInterfaces;
using SalifyCRM.IdentityServer.Application.Responses.UserOperationClaims;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalifyCRM.IdentityServer.Application.Handlers.Groups.Commands
{
    public class DeleteGroupCommand : IRequest<IResult>
    {
        public int DeletedUserId { get; set; }
        public int Id { get; set; }

        public class DeleteGroupCommandHandler : IRequestHandler<DeleteGroupCommand, IResult>
        {
            private readonly IGroupRepository _groupRepository;
            private readonly IMediator _mediator;

            public DeleteGroupCommandHandler(IGroupRepository groupRepository, IMediator mediator)
            {
                this._groupRepository = groupRepository;
                this._mediator = mediator;
            }

            public async Task<IResult> Handle(DeleteGroupCommand request, CancellationToken cancellationToken)
            {
                if (request.Id == null || request.DeletedUserId == null)
                {
                    return new ErrorResult("Değer null olamaz.");
                }

                if (request.Id < 0 || request.DeletedUserId < 0)
                {
                    return new ErrorResult("Değer negatif olamaz.");
                }

                var group = await _groupRepository.GetAsync(O => O.Id == request.Id && O.IsDeleted == false);
                if (group == null)
                {
                    return new ErrorResult("Kayıt bulunamadı ya da zaten silinmiş.");
                }
                group.IsDeleted = true;
                group.LastUpdatedDate = DateTime.UtcNow;
                group.LastUpdatedUserId = request.DeletedUserId;

                _groupRepository.Update(group);
                _groupRepository.SaveChanges();

                return new SuccessResult();
            }
        }
    }
}
