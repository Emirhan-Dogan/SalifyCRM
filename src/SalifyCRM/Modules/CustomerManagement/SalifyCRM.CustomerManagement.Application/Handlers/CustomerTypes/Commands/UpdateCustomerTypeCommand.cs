using Core.Utilities.Messages;
using Core.Utilities.Results;
using MediatR;
using SalifyCRM.CustomerManagement.Application.Handlers.CustomerTypes.ValidationRules;
using SalifyCRM.CustomerManagement.Application.Handlers.SocialMedias.ValidationRules;
using SalifyCRM.CustomerManagement.Application.RepositoryInterfaces;
using SalifyCRM.CustomerManagement.Application.Responses.SocialMedias;
using SalifyCRM.CustomerManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalifyCRM.CustomerManagement.Application.Handlers.CustomerTypes.Commands
{
    public class UpdateCustomerTypeCommand : IRequest<IResult>
    {
        public int Id { get; set; }
        public int LastUpdatedUserId { get; set; }

        public string Name { get; set; }
        public string Descriptions { get; set; }
        public string Status { get; set; }

        public class UpdateCustomerTypeCommandHandler : IRequestHandler<UpdateCustomerTypeCommand, IResult>
        {
            private readonly ICustomerTypeRepository _customerTypeRepository;
            private readonly IUserRepository _userRepository;
            private readonly IMediator _mediator;

            public UpdateCustomerTypeCommandHandler(
                ICustomerTypeRepository customerTypeRepository,
                IUserRepository userRepository,
                IMediator mediator)
            {
                this._customerTypeRepository = customerTypeRepository;
                this._userRepository = userRepository;
                this._mediator = mediator;
            }

            public async Task<IResult> Handle(UpdateCustomerTypeCommand request, CancellationToken cancellationToken)
            {
                var validator = new UpdateCustomerTypeValidator();
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

                var customerType = await _customerTypeRepository.GetAsync(O => O.Id == request.Id && O.IsDeleted == false);
                if(customerType == null)
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

                customerType.Name = request.Name;
                customerType.Descriptions = request.Descriptions;
                customerType.Status = request.Status;

                customerType.LastUpdatedUserId = request.LastUpdatedUserId;
                customerType.LastUpdatedDate = DateTime.UtcNow;

                _customerTypeRepository.Update(customerType);
                return new SuccessResult("Request successful.");
            }
        }
    }
}
