using Core.Utilities.Messages;
using Core.Utilities.Results;
using MediatR;
using SalifyCRM.CustomerManagement.Application.Extensions;
using SalifyCRM.CustomerManagement.Application.RepositoryInterfaces;
using SalifyCRM.CustomerManagement.Application.Responses.Cities;
using SalifyCRM.CustomerManagement.Application.Responses.Customers;
using SalifyCRM.CustomerManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalifyCRM.CustomerManagement.Application.Handlers.Customers.Queries
{
    public class GetCustomerQuery : IRequest<IDataResult<CustomerDetailResponse>>
    {
        public int Id { get; set; }

        public class GetCustomerQueryHandler : IRequestHandler<GetCustomerQuery, IDataResult<CustomerDetailResponse>>
        {
            private readonly ICustomerRepository _customerRepository;
            private readonly IAddressRepository _addressRepository;
            private readonly ICustomerSocialRepository _customerSocialRepository;
            private readonly ICustomerPermissionRepository _customerPermissionRepository;
            private readonly ICustomerNoteRepository _customerNoteRepository;
            private readonly ICustomerTagRepository _customerTagRepository;
            private readonly IMediator _mediator;

            public GetCustomerQueryHandler(
                ICustomerRepository customerRepository,
                IAddressRepository addressRepository,
                ICustomerSocialRepository customerSocialRepository,
                ICustomerPermissionRepository customerPermissionRepository,
                ICustomerNoteRepository customerNoteRepository,
                ICustomerTagRepository customerTagRepository,
                IMediator mediator)
            {
                this._customerRepository = customerRepository;
                this._addressRepository = addressRepository;
                this._customerSocialRepository = customerSocialRepository;
                this._customerPermissionRepository = customerPermissionRepository;
                this._customerNoteRepository = customerNoteRepository;
                this._customerTagRepository = customerTagRepository;
                this._mediator = mediator;
            }

            public async Task<IDataResult<CustomerDetailResponse>> Handle(GetCustomerQuery request, CancellationToken cancellationToken)
            {
                if (request.Id <= 0)
                {
                    return new ErrorDataResult<CustomerDetailResponse>()
                        .AddErrorDetail(new ErrorDetail
                        {
                            Code = ErrorCode.VAL1002,
                            Message = "The provided value should not be null or negative.",
                            Title = "Null or Negative Value Error"
                        }
                        .AddMetadata("FieldName", "Id")
                        .AddMetadata("InputValue", request.Id.ToString())
                        );
                }

                var customer = await _customerRepository.GetAsync(O => O.Id == request.Id && O.IsDeleted == false);
                if (customer == null)
                {
                    return new ErrorDataResult<CustomerDetailResponse>()
                         .AddErrorDetail(new ErrorDetail
                         {
                             Code = ErrorCode.BUS4003,
                             Message = "No customer was found with the provided ID.",
                             Title = "Customer Not Found"
                         }
                        .AddMetadata("FieldName", "Id")
                        .AddMetadata("InputValue", request.Id.ToString())
                        );
                }

                var address = _addressRepository.GetAddressesByCustomerId(request.Id);
                var customerSocials = await _customerSocialRepository.GetListAsync(O => O.CustomerId == request.Id && O.IsDeleted == false);
                var customerPermissions = await _customerPermissionRepository.GetListAsync(O => O.CustomerId == request.Id && O.IsDeleted == false);
                var customerNotes = await _customerNoteRepository.GetListAsync(O => O.CustomerId == request.Id && O.IsDeleted == false);
                var customerTags = _customerTagRepository.GetCustomerTagByCustomerId(request.Id);

                CustomerDetailResponse customerDetailResponse = 
                    CustomerExtensions.ToCustomerDetailResponse(customer, address, customerSocials.ToList(), customerPermissions.ToList(), customerNotes.ToList(), customerTags);

                return new SuccessDataResult<CustomerDetailResponse>(customerDetailResponse);
            }
        }
    }
}
