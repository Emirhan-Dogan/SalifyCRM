using Core.Utilities.Results;
using MediatR;
using SalifyCRM.CustomerManagement.Application.Extensions;
using SalifyCRM.CustomerManagement.Application.RepositoryInterfaces;
using SalifyCRM.CustomerManagement.Application.Responses.AddressTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalifyCRM.CustomerManagement.Application.Handlers.AddressTypes.Queries
{
    public class GetAddressTypesQuery : IRequest<IDataResult<List<AddressTypeResponse>>>
    {
        public class GetAddressTypesQueryHandler : IRequestHandler<GetAddressTypesQuery, IDataResult<List<AddressTypeResponse>>>
        {
            private readonly IAddressTypeRepository _addressTypeRepository;
            private readonly IMediator _mediator;

            public GetAddressTypesQueryHandler(IAddressTypeRepository addressTypeRepository, IMediator mediator)
            {
                this._addressTypeRepository = addressTypeRepository;
                this._mediator = mediator;
            }

            public async Task<IDataResult<List<AddressTypeResponse>>> Handle(GetAddressTypesQuery request, CancellationToken cancellationToken)
            {
                var addressTypes = await _addressTypeRepository.GetListAsync(x => x.IsDeleted == false);

                return new SuccessDataResult<List<AddressTypeResponse>>(
                    AddressTypeExtensions.ToAddressTypeResponseList(addressTypes.ToList())
                    );

            }
        }
    }
}
