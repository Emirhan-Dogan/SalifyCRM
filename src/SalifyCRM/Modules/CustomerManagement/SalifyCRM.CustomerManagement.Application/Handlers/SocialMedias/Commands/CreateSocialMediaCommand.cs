using Core.Utilities.Messages;
using Core.Utilities.Results;
using MediatR;
using SalifyCRM.CustomerManagement.Application.Handlers.SocialMedias.ValidationRules;
using SalifyCRM.CustomerManagement.Application.RepositoryInterfaces;
using SalifyCRM.CustomerManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalifyCRM.CustomerManagement.Application.Handlers.SocialMedias.Commands
{
    public class CreateSocialMediaCommand : IRequest<IResult>
    {
        public int CreatedUserId { get; set; }

        public string Name { get; set; }
        public string Icon { get; set; }
        public string Status { get; set; }

        public class CreateSocialMediaCommandHandler : IRequestHandler<CreateSocialMediaCommand, IResult>
        {
            private readonly ISocialMediaRepository _socialMediaRepository;
            private readonly IUserRepository _userRepository;
            private readonly IMediator _mediator;

            public CreateSocialMediaCommandHandler(
                ISocialMediaRepository socialMediaRepository,
                IUserRepository userRepository,
                IMediator mediator)
            {
                this._socialMediaRepository = socialMediaRepository;
                this._userRepository = userRepository;
                this._mediator = mediator;
            }

            public async Task<IResult> Handle(CreateSocialMediaCommand request, CancellationToken cancellationToken)
            {
                var validator = new CreateSocialMediaValidator();
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

                SocialMedia socialMedia = new SocialMedia()
                {
                    CreatedUserId = request.CreatedUserId,
                    CreatedDate = DateTime.UtcNow,
                    IsDeleted = false,
                    LastUpdatedUserId = request.CreatedUserId,
                    LastUpdatedDate = DateTime.UtcNow,
                    Name = request.Name,
                    Icon = request.Icon,
                    Status = request.Status,
                };

                _socialMediaRepository.Add(socialMedia);
                return new SuccessResult("Request successful.");
            }
        }
    }
}
