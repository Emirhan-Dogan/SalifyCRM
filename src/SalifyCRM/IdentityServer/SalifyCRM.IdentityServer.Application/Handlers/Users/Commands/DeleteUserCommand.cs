using Core.Utilities.Results;
using MediatR;
using SalifyCRM.IdentityServer.Application.RepositoryInterfaces;
using SalifyCRM.IdentityServer.Application.Responses.UserOperationClaims;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalifyCRM.IdentityServer.Application.Handlers.Users.Commands
{
    public class DeleteUserCommand : IRequest<IResult>
    {
        public int DeletedUserId { get; set; }
        public int Id { get; set; }

        public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, IResult>
        {
            private readonly IUserRepository _userRepository;
            private readonly IMediator _mediator;

            public DeleteUserCommandHandler(IUserRepository userRepository, IMediator mediator)
            {
                this._userRepository = userRepository;
                this._mediator = mediator;
            }

            public async Task<IResult> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
            {
                if (request.Id == null || request.DeletedUserId == null)
                {
                    return new ErrorResult("Değer null olamaz.");
                }

                if (request.Id < 0 || request.DeletedUserId < 0)
                {
                    return new ErrorResult("Değer negatif olamaz.");
                }

                var user = await _userRepository.GetAsync(O => O.Id == request.Id && O.IsDeleted == false);
                if(user == null)
                {
                    return new ErrorResult("Kayıt bulunamadı ya da zaten silinmiş.");
                }
                user.IsDeleted = true;
                user.LastUpdatedUserId = request.DeletedUserId;
                user.LastUpdatedDate = DateTime.UtcNow;

                _userRepository.Update(user);
                _userRepository.SaveChanges();

                return new SuccessResult();
            }
        }
    }
}
