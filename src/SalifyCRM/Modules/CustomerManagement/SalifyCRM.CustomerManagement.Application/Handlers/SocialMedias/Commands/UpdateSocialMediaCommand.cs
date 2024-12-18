using Core.Utilities.Messages;
using Core.Utilities.Results;
using MediatR;
using SalifyCRM.CustomerManagement.Application.Handlers.SocialMedias.ValidationRules;
using SalifyCRM.CustomerManagement.Application.RepositoryInterfaces;
using SalifyCRM.CustomerManagement.Application.Responses.SocialMedias;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalifyCRM.CustomerManagement.Application.Handlers.SocialMedias.Commands
{
    public class UpdateSocialMediaCommand : IRequest<IResult>
    {
        public int Id { get; set; }
        public int LastUpdatedUserId { get; set; }

        public string Name { get; set; }
        public string Icon { get; set; }
        public string Status { get; set; }

        public class UpdateSocialMediaCommandHandler : IRequestHandler<UpdateSocialMediaCommand, IResult>
        {
            private readonly ISocialMediaRepository _socialMediaRepository;
            private readonly IUserRepository _userRepository;
            private readonly IMediator _mediator;

            public UpdateSocialMediaCommandHandler(
                ISocialMediaRepository socialMediaRepository, 
                IUserRepository userRepository,
                IMediator mediator)
            {
                this._socialMediaRepository = socialMediaRepository;
                this._userRepository = userRepository;
                this._mediator = mediator;
            }

            public async Task<IResult> Handle(UpdateSocialMediaCommand request, CancellationToken cancellationToken)
            {
                var validator = new UpdateSocialMediaValidator();
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

                var isThereUserRecord = _userRepository.Query().Any(u => u.Id == request.LastUpdatedUserId);
                if (!isThereUserRecord)
                {
                    return new ErrorResult()
                         .AddErrorDetail(new ErrorDetail
                         {
                             Code = ErrorCode.BUS4003,
                             Message = "No user was found with the provided ID.",
                             Title = "User Not Found"
                         }
                        .AddMetadata("FieldName", "LastUpdatedUserId")
                        .AddMetadata("InputValue", request.LastUpdatedUserId.ToString())
                        );
                }

                var socialMedia = await _socialMediaRepository.GetAsync(O => O.Id == request.Id && O.IsDeleted == false);
                if(socialMedia == null)
                {
                    return new ErrorDataResult<SocialMediaDetailResponse>()
                        .AddErrorDetail(new ErrorDetail
                        {
                            Code = ErrorCode.BUS4002,
                            Message = "The requested data could not be found.",
                            Title = "Data Not Found Error"
                        }
                        .AddMetadata("FieldName", "Id")
                        .AddMetadata("InputValue", request.Id.ToString())
                        .AddMetadata("Suggestion", "A deleted or non-existing record was requested.")
                        );
                }

                socialMedia.Name = request.Name;
                socialMedia.Icon = request.Icon;
                socialMedia.Status = request.Status;

                socialMedia.LastUpdatedUserId = request.LastUpdatedUserId;
                socialMedia.LastUpdatedDate = DateTime.UtcNow;

                _socialMediaRepository.Update(socialMedia);
                return new SuccessResult("Request successful.");
            }
        }
    }
}
