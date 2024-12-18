using Core.Utilities.Messages;
using Core.Utilities.Results;
using MediatR;
using SalifyCRM.CustomerManagement.Application.Extensions;
using SalifyCRM.CustomerManagement.Application.RepositoryInterfaces;
using SalifyCRM.CustomerManagement.Application.Responses.Addresses;
using SalifyCRM.CustomerManagement.Application.Responses.CustomerAddresses;
using SalifyCRM.CustomerManagement.Application.Responses.CustomerSocials;
using SalifyCRM.CustomerManagement.Application.Responses.CustomerTagMappings;
using SalifyCRM.CustomerManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalifyCRM.CustomerManagement.Application.Handlers.CustomerAddresses.Queries
{
    public class GetAddressesForCustomerQurey : IRequest<IDataResult<List<CustomerAddressResponse>>>
    {
        public int CustomerId { get; set; }

        public class GetAddressesForCustomerQureyHandler : IRequestHandler<GetAddressesForCustomerQurey, IDataResult<List<CustomerAddressResponse>>>
        {
            private readonly ICustomerAddressRepository _customerAddressRepository;
            private readonly ICustomerRepository _customerRepository;
            private readonly IMediator _mediator;

            public GetAddressesForCustomerQureyHandler(
                ICustomerAddressRepository customerAddressRepository,
                ICustomerRepository customerRepository,
                IMediator mediator)
            {
                this._customerAddressRepository = customerAddressRepository;
                this._customerRepository = customerRepository;
                this._mediator = mediator;
            }

            public async Task<IDataResult<List<CustomerAddressResponse>>> Handle(GetAddressesForCustomerQurey request, CancellationToken cancellationToken)
            {
                if (request.CustomerId <= 0)
                {
                    return new ErrorDataResult<List<CustomerAddressResponse>>()
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

                var isThereCustomerRecord = _customerRepository.Query().Any(O => O.Id == request.CustomerId && O.IsDeleted == false);
                if (!isThereCustomerRecord)
                {
                    return new ErrorDataResult<List<CustomerAddressResponse>>()
                         .AddErrorDetail(new ErrorDetail
                         {
                             Code = ErrorCode.BUS4003,
                             Message = "No customer was found with the provided ID.",
                             Title = "Customer Not Found"
                         }
                        .AddMetadata("FieldName", "CustomerId")
                        .AddMetadata("InputValue", request.CustomerId.ToString())
                        );
                }

                var addresses = await _customerAddressRepository.GetListAsync(O => O.CustomerId == request.CustomerId && O.IsDeleted == false);

                List<CustomerAddressResponse> addressResponses = CustomerAddressExtensions.ToCustomerAddressResponseList(addresses.ToList());

                return new SuccessDataResult<List<CustomerAddressResponse>>(addressResponses);
            }
        }
    }
}
