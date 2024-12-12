using Core.Utilities.Results;
using MediatR;
using SalifyCRM.IdentityServer.Application.Handlers.Groups.ValidationRules;
using SalifyCRM.IdentityServer.Application.Handlers.UserGroups.ValidationRules;
using SalifyCRM.IdentityServer.Application.RepositoryInterfaces;
using SalifyCRM.IdentityServer.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalifyCRM.IdentityServer.Application.Handlers.UserGroups.Commands
{
    public class CreateUserGroupCommand : IRequest<IResult>
    {
        public int CreatedUserId { get; set; }

        public int UserId { get; set; }
        public int GroupId { get; set; }
        public bool Status { get; set; }

        public class CreateUserGroupCommandHandler : IRequestHandler<CreateUserGroupCommand, IResult>
        {
            private readonly IUserGroupRepository _userGroupRepository;
            private readonly IMediator _mediator;

            public CreateUserGroupCommandHandler(IUserGroupRepository userGroupRepository, IMediator mediator)
            {
                this._userGroupRepository = userGroupRepository;
                this._mediator = mediator;
            }

            public async Task<IResult> Handle(CreateUserGroupCommand request, CancellationToken cancellationToken)
            {
                var validator = new CreateUserGroupValidator();
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

                UserGroup userGroupRecord = await _userGroupRepository.GetAsync(O => O.GroupId == request.GroupId && O.UserId == request.UserId);

                if (userGroupRecord != null)
                {
                    userGroupRecord.Status = request.Status;
                    userGroupRecord.IsDeleted = false;
                    userGroupRecord.LastUpdatedDate = DateTime.UtcNow;
                    userGroupRecord.LastUpdatedUserId = request.CreatedUserId;

                    _userGroupRepository.Update(userGroupRecord);
                    _userGroupRepository.SaveChanges();

                    return new SuccessResult();
                }

                UserGroup userGroup = new UserGroup()
                {
                    GroupId = request.GroupId,
                    UserId = request.UserId,
                    Status = request.Status,
                    IsDeleted = false,
                    CreatedDate = DateTime.UtcNow,
                    LastUpdatedDate = DateTime.UtcNow,
                    CreatedUserId = request.CreatedUserId,
                    LastUpdatedUserId = request.CreatedUserId
                };

                _userGroupRepository.Update(userGroup);
                _userGroupRepository.SaveChanges();

                return new SuccessResult();
            }
        }
    }
}
