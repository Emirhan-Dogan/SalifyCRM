using Core.Utilities.Messages;
using Core.Utilities.Results;
using MediatR;
using SalifyCRM.CustomerManagement.Application.Handlers.MartialStatuses.ValidationRules;
using SalifyCRM.CustomerManagement.Application.Handlers.SocialMedias.ValidationRules;
using SalifyCRM.CustomerManagement.Application.RepositoryInterfaces;
using SalifyCRM.CustomerManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalifyCRM.CustomerManagement.Application.Handlers.MartialStatuses.Commands
{
    public class CreateMartialStatusCommand : IRequest<IResult>
    {
        public int CreatedUserId { get; set; }

        public string Name { get; set; }
        public string Descriptions { get; set; }
        public string Status { get; set; }

        public class CreateMartialStatusCommandHandler : IRequestHandler<CreateMartialStatusCommand, IResult>
        {
            private readonly IMartialStatusRepository _martialStatusRepository;
            private readonly IUserRepository _userRepository;
            private readonly IMediator _mediator;

            public CreateMartialStatusCommandHandler(
                IMartialStatusRepository martialStatusRepository,
                IUserRepository userRepository,
                IMediator mediator)
            {
                this._martialStatusRepository = martialStatusRepository;
                this._userRepository = userRepository;
                this._mediator = mediator;
            }

            public async Task<IResult> Handle(CreateMartialStatusCommand request, CancellationToken cancellationToken)
            {
                var validator = new CreateMartialStatusValidator();
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

                MartialStatus martialStatus = new MartialStatus()
                {
                    CreatedUserId = request.CreatedUserId,
                    CreatedDate = DateTime.UtcNow,
                    LastUpdatedDate = DateTime.UtcNow,
                    LastUpdatedUserId = request.CreatedUserId,
                    IsDeleted = false,
                    Name = request.Name,
                    Descriptions = request.Descriptions,
                    Status = request.Status
                };

                _martialStatusRepository.Add(martialStatus);
                return new SuccessResult("Request successful.");
            }
        }
    }
}
