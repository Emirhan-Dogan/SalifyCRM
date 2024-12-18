using Core.Utilities.Results;
using MediatR;
using SalifyCRM.CustomerManagement.Application.Extensions;
using SalifyCRM.CustomerManagement.Application.RepositoryInterfaces;
using SalifyCRM.CustomerManagement.Application.Responses.Addresses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalifyCRM.CustomerManagement.Application.Handlers.Addresses.Queries
{
    public class GetAddressesQuery : IRequest<IDataResult<List<AddressResponse>>>
    {
        public class GetAddressesQueryHandler : IRequestHandler<GetAddressesQuery, IDataResult<List<AddressResponse>>>
        {
            private readonly IAddressRepository _addressRepository;
            private readonly IMediator _mediator;

            public GetAddressesQueryHandler(
                IAddressRepository addressRepository,
                IMediator mediator)
            {
                this._addressRepository = addressRepository;
                this._mediator = mediator;
            }

            public async Task<IDataResult<List<AddressResponse>>> Handle(GetAddressesQuery request, CancellationToken cancellationToken)
            {
                var addresses = await _addressRepository.GetListAsync(O => O.IsDeleted == false);

                List<AddressResponse> addressResponses = AddressExtensions.ToAddressResponseList(addresses.ToList());

                return new SuccessDataResult<List<AddressResponse>>(addressResponses);
            }
        }
    }
}
