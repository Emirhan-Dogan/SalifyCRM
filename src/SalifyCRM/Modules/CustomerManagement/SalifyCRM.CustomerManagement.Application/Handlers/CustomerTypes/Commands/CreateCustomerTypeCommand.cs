using Core.Utilities.Messages;
using Core.Utilities.Results;
using MediatR;
using SalifyCRM.CustomerManagement.Application.Handlers.CustomerTypes.ValidationRules;
using SalifyCRM.CustomerManagement.Application.Handlers.SocialMedias.ValidationRules;
using SalifyCRM.CustomerManagement.Application.RepositoryInterfaces;
using SalifyCRM.CustomerManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalifyCRM.CustomerManagement.Application.Handlers.CustomerTypes.Commands
{
    public class CreateCustomerTypeCommand : IRequest<IResult>
    {
        public int CreatedUserId { get; set; }

        public string Name { get; set; }
        public string Descriptions { get; set; }
        public string Status { get; set; }


        public class CreateCustomerTypeCommandHandler : IRequestHandler<CreateCustomerTypeCommand, IResult>
        {
            private readonly ICustomerTypeRepository _customerTypeRepository;
            private readonly IUserRepository _userRepository;
            private readonly IMediator _mediator;

            public CreateCustomerTypeCommandHandler(
                ICustomerTypeRepository customerTypeRepository,
                IUserRepository userRepository,
                IMediator mediator)
            {
                this._customerTypeRepository = customerTypeRepository;
                this._userRepository = userRepository;
                this._mediator = mediator;
            }

            public async Task<IResult> Handle(CreateCustomerTypeCommand request, CancellationToken cancellationToken)
            {
                var validator = new CreateCustomerTypeValidator();
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

                CustomerType customerType = new CustomerType()
                {
                    CreatedUserId = request.CreatedUserId,
                    LastUpdatedUserId = request.CreatedUserId,
                    CreatedDate = DateTime.UtcNow,
                    LastUpdatedDate = DateTime.UtcNow,
                    IsDeleted = false,
                    Name = request.Name,
                    Status = request.Status,
                    Descriptions = request.Descriptions
                };

                _customerTypeRepository.Add(customerType);
                return new SuccessResult("Request successful.");
            }
        }
    }
}
