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

namespace SalifyCRM.CustomerManagement.Application.Handlers.CustomerSocials.Commands
{
    public class AddSocialMediaToCustomerCommand : IRequest<IResult>
    {
        public int CreatedUserId { get; set; }
        public int SocialMediaId { get; set; }
        public int CustomerId { get; set; }

        public string SocialMediaLink { get; set; }

        public class AddSocialMediaToCustomerCommandHandler : IRequestHandler<AddSocialMediaToCustomerCommand, IResult>
        {
            private readonly ICustomerSocialRepository _customerSocialRepository;
            private readonly ICustomerRepository _customerRepository;
            private readonly ISocialMediaRepository _socialMediaRepository;
            private readonly IUserRepository _userRepository;
            private readonly IMediator _mediator;

            public AddSocialMediaToCustomerCommandHandler(
                ICustomerSocialRepository customerSocialRepository,
                ICustomerRepository customerRepository,
                ISocialMediaRepository socialMediaRepository,
                IUserRepository userRepository,
                IMediator mediator)
            {
                this._customerSocialRepository = customerSocialRepository;
                this._customerRepository = customerRepository;
                this._socialMediaRepository = socialMediaRepository;
                this._userRepository = userRepository;
                this._mediator = mediator;
            }

            public async Task<IResult> Handle(AddSocialMediaToCustomerCommand request, CancellationToken cancellationToken)
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

                if (request.SocialMediaId <= 0)
                {
                    return new ErrorResult()
                        .AddErrorDetail(new ErrorDetail
                        {
                            Code = ErrorCode.VAL1002,
                            Message = "The provided value should not be null or negative.",
                            Title = "Null or Negative Value Error"
                        }
                        .AddMetadata("FieldName", "SocialMediaId")
                        .AddMetadata("InputValue", request.SocialMediaId.ToString())
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

                var isThereSocialMediaRecord = _socialMediaRepository.Query().Any(O => O.Id == request.SocialMediaId && O.IsDeleted == false);
                if (!isThereSocialMediaRecord)
                {
                    return new ErrorResult()
                         .AddErrorDetail(new ErrorDetail
                         {
                             Code = ErrorCode.BUS4003,
                             Message = "No social media was found with the provided SocialMediaId.",
                             Title = "Social Media Not Found"
                         }
                        .AddMetadata("FieldName", "SocialMediaId")
                        .AddMetadata("InputValue", request.SocialMediaId.ToString())
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

                var customerSocial = await _customerSocialRepository.GetAsync(O=>O.CustomerId == request.CustomerId && O.SocialMediaId == request.SocialMediaId);
                if (customerSocial != null)
                {
                    customerSocial.IsDeleted = false;
                    customerSocial.LastUpdatedUserId = request.CreatedUserId;
                    customerSocial.LastUpdatedDate = DateTime.UtcNow;
                    customerSocial.SocialMediaLink = request.SocialMediaLink;

                    _customerSocialRepository.Update(customerSocial);
                    return new SuccessResult("Request Successful.");
                }

                CustomerSocial newCustomerSocial = new CustomerSocial()
                {
                    SocialMediaId = request.SocialMediaId,
                    CustomerId = request.CustomerId,
                    CreatedDate = DateTime.UtcNow,
                    CreatedUserId = request.CreatedUserId,
                    LastUpdatedDate = DateTime.UtcNow,
                    LastUpdatedUserId = request.CreatedUserId,
                    IsDeleted = false,
                    SocialMediaLink = request.SocialMediaLink
                };

                _customerSocialRepository.Add(newCustomerSocial);

                return new SuccessResult("Request Successful.");
            }
        }
    }
}
