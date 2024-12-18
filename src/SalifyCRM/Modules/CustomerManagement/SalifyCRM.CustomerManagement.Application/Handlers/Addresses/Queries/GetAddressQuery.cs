using Core.Utilities.Messages;
using Core.Utilities.Results;
using MediatR;
using SalifyCRM.CustomerManagement.Application.Extensions;
using SalifyCRM.CustomerManagement.Application.RepositoryInterfaces;
using SalifyCRM.CustomerManagement.Application.Responses.Addresses;
using SalifyCRM.CustomerManagement.Application.Responses.AddressTypes;
using SalifyCRM.CustomerManagement.Application.Responses.Cities;
using SalifyCRM.CustomerManagement.Application.Responses.Countries;
using SalifyCRM.CustomerManagement.Application.Responses.CustomerTagMappings;
using SalifyCRM.CustomerManagement.Application.Responses.Districts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalifyCRM.CustomerManagement.Application.Handlers.Addresses.Queries
{
    public class GetAddressQuery : IRequest<IDataResult<AddressDetailResponse>>
    {
        public int Id { get; set; }

        public class GetAddressQueryHandler : IRequestHandler<GetAddressQuery, IDataResult<AddressDetailResponse>>
        {
            private readonly IAddressRepository _addressRepository;
            private readonly IMediator _mediator;

            public GetAddressQueryHandler(
                IAddressRepository addressRepository,
                IMediator mediator)
            {
                this._addressRepository = addressRepository;
                this._mediator = mediator;
            }

            public async Task<IDataResult<AddressDetailResponse>> Handle(GetAddressQuery request, CancellationToken cancellationToken)
            {
                if (request.Id <= 0)
                {
                    return new ErrorDataResult<AddressDetailResponse>()
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

                var address = _addressRepository.Get(O => O.Id == request.Id && O.IsDeleted == false);
                if (address == null)
                {
                    return new ErrorDataResult<AddressDetailResponse>()
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
                AddressDetailResponse addressDetailResponse = AddressExtensions.ToAddressDetailResponse(address);

                return new SuccessDataResult<AddressDetailResponse>(addressDetailResponse);
            }
        }
    }
}
