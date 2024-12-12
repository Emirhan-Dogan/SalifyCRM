using Core.Utilities.Results;
using MediatR;
using SalifyCRM.IdentityServer.Application.RepositoryInterfaces;
using SalifyCRM.IdentityServer.Application.Responses.OperationClaims;
using SalifyCRM.IdentityServer.Application.Responses.UserOperationClaims;
using SalifyCRM.IdentityServer.Application.Responses.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalifyCRM.IdentityServer.Application.Handlers.UserOperationClaims.Queries
{
    public class GetUserOperationClaimQuery : IRequest<IDataResult<UserOperationClaimDetailResponse>>
    {
        public int Id { get; set; }

        public class GetUserOperationClaimQueryHandler : IRequestHandler<GetUserOperationClaimQuery, IDataResult<UserOperationClaimDetailResponse>>
        {
            private readonly IUserOperationClaimRepository _userOperationClaimRepository;
            private readonly IUserRepository _userRepository;
            private readonly IOperationClaimRepository _operationClaimRepository;
            private readonly IMediator _mediator;

            public GetUserOperationClaimQueryHandler(
                IUserOperationClaimRepository userOperationClaimRepository,
                IUserRepository userRepository,
                IOperationClaimRepository operationClaimRepository,
                IMediator mediator)
            {
                this._userOperationClaimRepository = userOperationClaimRepository;
                this._userRepository = userRepository;
                this._operationClaimRepository = operationClaimRepository;
                this._mediator = mediator;
            }

            public async Task<IDataResult<UserOperationClaimDetailResponse>> Handle(GetUserOperationClaimQuery request, CancellationToken cancellationToken)
            {
                if (request.Id == null)
                {
                    return new ErrorDataResult<UserOperationClaimDetailResponse>("Id değeri null olamaz.");
                }

                if (request.Id < 0)
                {
                    return new ErrorDataResult<UserOperationClaimDetailResponse>("Id değeri negatif olamaz.");
                }

                var userOperationClaim = await _userOperationClaimRepository.GetAsync(O => O.Id == request.Id && O.IsDeleted == false);
                if (userOperationClaim == null)
                {
                    return new ErrorDataResult<UserOperationClaimDetailResponse>("Kayıt bulunamadı");
                }

                var user = await _userRepository.GetAsync(O => O.Id == userOperationClaim.UserId && O.IsDeleted == false);
                var operationClaim = await _operationClaimRepository.GetAsync(O => O.Id == userOperationClaim.OperationClaimId && O.IsDeleted == false);

                UserOperationClaimDetailResponse result = new UserOperationClaimDetailResponse()
                {
                    OperationClaimId = userOperationClaim.OperationClaimId,
                    Status = userOperationClaim.Status,
                    Id = userOperationClaim.Id,
                    UserId = userOperationClaim.Id,
                    OperationClaim = new OperationClaimResponse()
                    {
                        Id = operationClaim.Id,
                        Descriptions = operationClaim.Descriptions,
                        Name = operationClaim.Name
                    },
                    User = new UserResponse()
                    {
                        Id = user.Id,
                        BirthDate = user.BirthDate,
                        CitizenId = user.CitizenId,
                        Email = user.Email,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Gender = user.Gender,
                        LastLoginDate = user.LastLoginDate,
                        Notes = user.Notes,
                        PhoneNumber = user.PhoneNumber,
                        ProfileImage = user.ProfileImage,
                        Status = user.Status
                    }
                };

                return new SuccessDataResult<UserOperationClaimDetailResponse>(result);
            }
        }
    }
}
