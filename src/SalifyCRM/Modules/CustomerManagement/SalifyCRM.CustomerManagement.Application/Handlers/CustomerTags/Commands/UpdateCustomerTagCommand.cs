using Core.Utilities.Messages;
using Core.Utilities.Results;
using MediatR;
using SalifyCRM.CustomerManagement.Application.Handlers.CustomerTags.ValidationRules;
using SalifyCRM.CustomerManagement.Application.Handlers.SocialMedias.ValidationRules;
using SalifyCRM.CustomerManagement.Application.RepositoryInterfaces;
using SalifyCRM.CustomerManagement.Application.Responses.SocialMedias;
using SalifyCRM.CustomerManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalifyCRM.CustomerManagement.Application.Handlers.CustomerTags.Commands
{
    public class UpdateCustomerTagCommand : IRequest<IResult>
    {
        public int Id { get; set; }
        public int LastUpdatedUserId { get; set; }

        public string Name { get; set; }
        public string Descriptions { get; set; }
        public string Status { get; set; }

        public class UpdateCustomerTagCommandHandler : IRequestHandler<UpdateCustomerTagCommand, IResult>
        {
            private readonly ICustomerTagRepository _customerTagRepository;
            private readonly IUserRepository _userRepository;
            private readonly IMediator _mediator;

            public UpdateCustomerTagCommandHandler(
                ICustomerTagRepository customerTagRepository,
                IUserRepository userRepository,
                IMediator mediator)
            {
                this._customerTagRepository = customerTagRepository;
                this._userRepository = userRepository;
                this._mediator = mediator;
            }

            public async Task<IResult> Handle(UpdateCustomerTagCommand request, CancellationToken cancellationToken)
            {
                var validator = new UpdateCustomerTagValidator();
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

                var customerTag = await _customerTagRepository.GetAsync(O => O.Id == request.Id && O.IsDeleted == false);
                if (customerTag == null)
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

                customerTag.Name = request.Name;
                customerTag.Descriptions = request.Descriptions;
                customerTag.Status = request.Status;

                customerTag.LastUpdatedUserId = request.LastUpdatedUserId;
                customerTag.LastUpdatedDate = DateTime.UtcNow;

                _customerTagRepository.Update(customerTag);
                return new SuccessResult("Request successful.");
            }
        }
    }
}
