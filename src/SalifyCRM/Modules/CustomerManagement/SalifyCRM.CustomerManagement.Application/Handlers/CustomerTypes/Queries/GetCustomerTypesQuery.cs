using Core.Utilities.Results;
using MediatR;
using SalifyCRM.CustomerManagement.Application.Extensions;
using SalifyCRM.CustomerManagement.Application.RepositoryInterfaces;
using SalifyCRM.CustomerManagement.Application.Responses.CustomerTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalifyCRM.CustomerManagement.Application.Handlers.CustomerTypes.Queries
{
    public class GetCustomerTypesQuery : IRequest<IDataResult<List<CustomerTypeResponse>>>
    {
        public class GetCustomerTypesQueryHandler : IRequestHandler<GetCustomerTypesQuery, IDataResult<List<CustomerTypeResponse>>>
        {
            private readonly ICustomerTypeRepository _customerTypeRepository;
            private readonly IMediator _mediator;

            public GetCustomerTypesQueryHandler(ICustomerTypeRepository customerTypeRepository, IMediator mediator)
            {
                this._customerTypeRepository = customerTypeRepository;
                this._mediator = mediator;
            }

            public async Task<IDataResult<List<CustomerTypeResponse>>> Handle(GetCustomerTypesQuery request, CancellationToken cancellationToken)
            {
                var customerTypes = await _customerTypeRepository.GetListAsync(O => O.IsDeleted == false);

                List<CustomerTypeResponse> result = CustomerTypeExtensions.ToCustomerTypeResponseList(customerTypes.ToList());

                return new SuccessDataResult<List<CustomerTypeResponse>>(result.ToList());
            }
        }
    }
}
