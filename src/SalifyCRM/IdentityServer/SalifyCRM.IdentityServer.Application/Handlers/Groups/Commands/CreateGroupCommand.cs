using Core.Utilities.Results;
using MediatR;
using SalifyCRM.IdentityServer.Application.Handlers.GroupClaims.ValidationRules;
using SalifyCRM.IdentityServer.Application.Handlers.Groups.ValidationRules;
using SalifyCRM.IdentityServer.Application.RepositoryInterfaces;
using SalifyCRM.IdentityServer.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalifyCRM.IdentityServer.Application.Handlers.Groups.Commands
{
    public class CreateGroupCommand : IRequest<IResult>
    {
        public int CreatedUserId { get; set; }

        public string Name { get; set; }
        public string? Descriptions { get; set; }
        public bool Status { get; set; }
        public List<int>? UsersId { get; set; }
        public List<int>? OperationClaimsId { get; set; }

        public class CreateGroupCommandHandler : IRequestHandler<CreateGroupCommand, IResult>
        {
            private readonly IGroupRepository _groupRepository;
            private readonly IUserGroupRepository _userGroupRepository;
            private readonly IGroupClaimRepository _groupClaimRepository;
            private readonly IMediator _mediator;

            public CreateGroupCommandHandler(
                IGroupClaimRepository groupClaimRepository,
                IUserGroupRepository userGroupRepository,
                IGroupRepository groupRepository,
                IMediator mediator)
            {
                this._groupClaimRepository = groupClaimRepository;
                this._userGroupRepository = userGroupRepository;
                this._groupRepository = groupRepository;
                this._mediator = mediator;
            }

            public async Task<IResult> Handle(CreateGroupCommand request, CancellationToken cancellationToken)
            {
                var validator = new CreateGroupValidator();
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

                var isThereUserRecord = _groupRepository.Query().Any(u => u.Name == request.Name);

                if (isThereUserRecord == true)
                    return new ErrorResult($"{request.Name} ismi ile zaten bir grup zaten var.");

                Group group = new Group()
                {
                    Name = request.Name,
                    Descriptions = request.Descriptions,
                    Status = request.Status,
                    LastUpdatedDate = DateTime.UtcNow,
                    CreatedDate = DateTime.UtcNow,
                    IsDeleted = false,
                    LastUpdatedUserId = request.CreatedUserId,
                    CreatedUserId = request.CreatedUserId
                };

                _groupRepository.Add(group);
                _groupRepository.SaveChanges();
                int groupId = _groupRepository.Get(O => O.Name == request.Name).Id;

                if(request.UsersId != null)
                {
                    _userGroupRepository.AddBulkUsersInGroup(request.UsersId, groupId, request.CreatedUserId);
                    _userGroupRepository.SaveChanges();
                }
                if(request.OperationClaimsId != null)
                {
                    _groupClaimRepository.AddBulkOperationClaimsInGroup(request.OperationClaimsId, groupId, request.CreatedUserId);
                    _groupClaimRepository.SaveChanges();
                }
                
                return new SuccessResult();
            }
        }
    }

}
