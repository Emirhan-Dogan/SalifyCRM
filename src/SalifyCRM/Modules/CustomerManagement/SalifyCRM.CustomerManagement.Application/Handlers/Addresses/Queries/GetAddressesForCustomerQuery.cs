using Core.Utilities.Messages;
using Core.Utilities.Results;
using MediatR;
using SalifyCRM.CustomerManagement.Application.Extensions;
using SalifyCRM.CustomerManagement.Application.RepositoryInterfaces;
using SalifyCRM.CustomerManagement.Application.Responses.Addresses;
using SalifyCRM.CustomerManagement.Application.Responses.CustomerSocials;
using SalifyCRM.CustomerManagement.Application.Responses.CustomerTagMappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalifyCRM.CustomerManagement.Application.Handlers.Addresses.Queries
{
    public class GetAddressesForCustomerQuery : IRequest<IDataResult<List<AddressResponse>>>
    {
        public int CustomerId { get; set; }

        public class GetAddressesForCustomerQueryHandler : IRequestHandler<GetAddressesForCustomerQuery, IDataResult<List<AddressResponse>>>
        {
            private readonly IAddressRepository _addressRepository;
            private readonly ICustomerRepository _customerRepository;
            private readonly IMediator _mediator;

            public GetAddressesForCustomerQueryHandler(
                IAddressRepository addressRepository,
                ICustomerRepository customerRepository,
                IMediator mediator)
            {
                this._addressRepository = addressRepository;
                this._customerRepository = customerRepository;
                this._mediator = mediator;
            }

            public async Task<IDataResult<List<AddressResponse>>> Handle(GetAddressesForCustomerQuery request, CancellationToken cancellationToken)
            {
                if (request.CustomerId <= 0)
                {
                    return new ErrorDataResult<List<AddressResponse>>()
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
                    return new ErrorDataResult<List<AddressResponse>>()
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

                var addresses = _addressRepository.GetAddressesByCustomerId(request.CustomerId);
                List<AddressResponse> addressResponses = AddressExtensions.ToAddressResponseList(addresses.ToList());

                return new SuccessDataResult<List<AddressResponse>>(addressResponses);
            }
        }
    }
}
