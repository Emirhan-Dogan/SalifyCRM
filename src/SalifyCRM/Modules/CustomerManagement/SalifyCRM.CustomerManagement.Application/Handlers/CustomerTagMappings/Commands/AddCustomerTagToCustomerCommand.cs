﻿using Core.Utilities.Messages;
using Core.Utilities.Results;
using MediatR;
using SalifyCRM.CustomerManagement.Application.RepositoryInterfaces;
using SalifyCRM.CustomerManagement.Application.Responses.Cities;
using SalifyCRM.CustomerManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalifyCRM.CustomerManagement.Application.Handlers.CustomerTagMappings.Commands
{
    public class AddCustomerTagToCustomerCommand : IRequest<IResult>
    {
        public int CreatedUserId { get; set; }
        public int CustomerTagId { get; set; }
        public int CustomerId { get; set; }

        public class AddCustomerTagToCustomerCommandHandler : IRequestHandler<AddCustomerTagToCustomerCommand, IResult>
        {
            private readonly ICustomerTagMappingRepository _customerTagMappingRepository;
            private readonly ICustomerTagRepository _customerTagRepository;
            private readonly ICustomerRepository _customerRepository;
            private readonly IUserRepository _userRepository;
            private readonly IMediator _mediator;

            public AddCustomerTagToCustomerCommandHandler(
                ICustomerTagMappingRepository customerTagMappingRepository,
                ICustomerTagRepository customerTagRepository,
                ICustomerRepository customerRepository,
                IUserRepository userRepository,
                IMediator mediator)
            {
                this._customerTagMappingRepository = customerTagMappingRepository;
                this._customerTagRepository = customerTagRepository;
                this._customerRepository = customerRepository;
                this._userRepository = userRepository;
                this._mediator = mediator;
            }

            public async Task<IResult> Handle(AddCustomerTagToCustomerCommand request, CancellationToken cancellationToken)
            {
                if (request.CustomerId <= 0)
                {
                    return new ErrorResult()
                        .AddErrorDetail(new ErrorDetail
                        {
                            Code = ErrorCode.VAL1002,
                            Message = "The provided value should not be null or negative.",
                            Title = "Null or Negative Value Error"
                        }
                        .AddMetadata("FieldName", "CustomerId")
                        .AddMetadata("InputValue", request.CustomerId.ToString())
                        );
                }

                if (request.CustomerTagId <= 0)
                {
                    return new ErrorResult()
                        .AddErrorDetail(new ErrorDetail
                        {
                            Code = ErrorCode.VAL1002,
                            Message = "The provided value should not be null or negative.",
                            Title = "Null or Negative Value Error"
                        }
                        .AddMetadata("FieldName", "CustomerTagId")
                        .AddMetadata("InputValue", request.CustomerTagId.ToString())
                        );
                }

                var isThereCustomerRecord = _customerRepository.Query().Any(O => O.Id == request.CustomerId && O.IsDeleted == false);
                if (!isThereCustomerRecord)
                {
                    return new ErrorResult()
                         .AddErrorDetail(new ErrorDetail
                         {
                             Code = ErrorCode.BUS4003,
                             Message = "No customer was found with the provided CustomerId.",
                             Title = "Customer Not Found"
                         }
                        .AddMetadata("FieldName", "CustomerId")
                        .AddMetadata("InputValue", request.CustomerId.ToString())
                        );
                }

                var isThereCustomerTagRecord = _customerTagRepository.Query().Any(O => O.Id == request.CustomerTagId && O.IsDeleted == false);
                if (!isThereCustomerTagRecord)
                {
                    return new ErrorResult()
                         .AddErrorDetail(new ErrorDetail
                         {
                             Code = ErrorCode.BUS4003,
                             Message = "No customer tag was found with the provided CustomerTagId.",
                             Title = "Customer Tag Not Found"
                         }
                        .AddMetadata("FieldName", "CustomerTagId")
                        .AddMetadata("InputValue", request.CustomerTagId.ToString())
                        );
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

                var customerTagToCustomer = await _customerTagMappingRepository.GetAsync(O=>O.CustomerId == request.CustomerId && O.CustomerTagId == request.CustomerTagId);
                if (customerTagToCustomer != null)
                {
                    customerTagToCustomer.IsDeleted = false;
                    customerTagToCustomer.LastUpdatedUserId = request.CreatedUserId;
                    customerTagToCustomer.LastUpdatedDate = DateTime.UtcNow;

                    _customerTagMappingRepository.Update(customerTagToCustomer);
                    return new SuccessResult("Request Successful.");
                }

                CustomerTagMapping newCustomerTagMapping = new CustomerTagMapping()
                {
                    CustomerTagId = request.CustomerTagId,
                    CustomerId = request.CustomerId,
                    CreatedDate = DateTime.UtcNow,
                    CreatedUserId = request.CreatedUserId,
                    LastUpdatedDate = DateTime.UtcNow,
                    LastUpdatedUserId = request.CreatedUserId,
                    IsDeleted = false
                };

                _customerTagMappingRepository.Add(newCustomerTagMapping);

                return new SuccessResult("Request Successful.");
            }
        }
    }
}
