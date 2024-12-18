using Core.Utilities.Messages;
using Core.Utilities.Results;
using MediatR;
using SalifyCRM.CustomerManagement.Application.Handlers.Permissions.ValidationRules;
using SalifyCRM.CustomerManagement.Application.Handlers.SocialMedias.ValidationRules;
using SalifyCRM.CustomerManagement.Application.RepositoryInterfaces;
using SalifyCRM.CustomerManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalifyCRM.CustomerManagement.Application.Handlers.Permissions.Commands
{
    public class CreatePermissionCommand : IRequest<IResult>
    {
        public int CreatedUserId { get; set; }

        public string Name { get; set; }
        public string Descriptions { get; set; }
        public string DocumentPath { get; set; }
        public string Status { get; set; }

        public class CreatePermissionCommandHandler : IRequestHandler<CreatePermissionCommand, IResult>
        {
            private readonly IPermissionRepository _permissionRepository;
            private readonly IUserRepository _userRepository;
            private readonly IMediator _mediator;

            public CreatePermissionCommandHandler(
                IPermissionRepository permissionRepository,
                IUserRepository userRepository,
                IMediator mediator)
            {
                this._permissionRepository = permissionRepository;
                this._userRepository = userRepository;
                this._mediator = mediator;
            }

            public async Task<IResult> Handle(CreatePermissionCommand request, CancellationToken cancellationToken)
            {
                var validator = new CreatePermissionValidator();
                var validationResult = await validator.ValidateAsync(request);

                if (!validationResult.IsValid)
                {
                    var errorResult = new ErrorResult();

                    foreach (var failure in validationResult.Errors)
                    {
                        errorResult.AddErrorDetail(new ErrorDetail
                        {
                            Code = ErrorCode.VAL1004,
                            Message = failure.ErrorMessage,
                            Title = "Validation Error"
                        }
                        .AddMetadata("FieldName", failure.PropertyName)
                        .AddMetadata("InputValue", failure.AttemptedValue?.ToString()));
                    }

                    return errorResult;
                }

                var isThereUserRecord = _userRepository.Query().Any(u => u.Id == request.CreatedUserId);
                if (!isThereUserRecord)
                {
                    return new ErrorResult()
                         .AddErrorDetail(new ErrorDetail
                         {
                             Code = ErrorCode.BUS4003,
                             Message = "No user was found with the provided ID.",
                             Title = "User Not Found"
                         }
                        .AddMetadata("FieldName", "CreatedUserId")
                        .AddMetadata("InputValue", request.CreatedUserId.ToString())
                        );
                }

                Permission permission = new Permission()
                {
                    CreatedUserId = request.CreatedUserId,
                    CreatedDate = DateTime.UtcNow,
                    IsDeleted = false,
                    LastUpdatedDate = DateTime.UtcNow,
                    LastUpdatedUserId = request.CreatedUserId,
                    Descriptions = request.Descriptions,
                    DocumentPath = request.DocumentPath,
                    Name = request.Name,
                    Status = request.Status
                };

                _permissionRepository.Add(permission);
                return new SuccessResult("Request successful.");
            }
        }
    }
}
