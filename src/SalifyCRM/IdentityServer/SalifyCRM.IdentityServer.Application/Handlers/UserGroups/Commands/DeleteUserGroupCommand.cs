using Core.Utilities.Results;
using MediatR;
using SalifyCRM.IdentityServer.Application.RepositoryInterfaces;
using SalifyCRM.IdentityServer.Application.Responses.UserOperationClaims;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalifyCRM.IdentityServer.Application.Handlers.UserGroups.Commands
{
    public class DeleteUserGroupCommand : IRequest<IResult>
    {
        public int DeletedUserId { get; set; }
        public int Id { get; set; }

        public class DeleteUserGroupCommandHandler : IRequestHandler<DeleteUserGroupCommand, IResult>
        {
            private readonly IUserGroupRepository _userGroupRepository;
            private readonly IMediator _mediator;

            public DeleteUserGroupCommandHandler(IUserGroupRepository userGroupRepository, IMediator mediator)
            {
                this._userGroupRepository = userGroupRepository;
                this._mediator = mediator;
            }

            public async Task<IResult> Handle(DeleteUserGroupCommand request, CancellationToken cancellationToken)
            {
                if (request.Id == null || request.DeletedUserId == null)
                {
                    return new ErrorResult("Değer null olamaz.");
                }

                if (request.Id < 0 || request.DeletedUserId < 0)
                {
                    return new ErrorResult("Değer negatif olamaz.");
                }

                var userGroup = await _userGroupRepository.GetAsync(O => O.Id == request.Id && O.IsDeleted == false);
                if(userGroup == null)
                {
                    return new ErrorResult("Kayıt bulunamadı ya da zaten silinmiş.");
                }

                userGroup.IsDeleted = true;
                userGroup.LastUpdatedDate = DateTime.UtcNow;
                userGroup.LastUpdatedUserId = request.DeletedUserId;

                _userGroupRepository.Update(userGroup);
                _userGroupRepository.SaveChanges();

                return new SuccessResult();
            }
        }
    }
}
