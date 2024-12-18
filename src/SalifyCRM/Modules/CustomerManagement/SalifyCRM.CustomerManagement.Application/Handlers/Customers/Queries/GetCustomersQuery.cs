using Core.Utilities.Results;
using MediatR;
using SalifyCRM.CustomerManagement.Application.Extensions;
using SalifyCRM.CustomerManagement.Application.RepositoryInterfaces;
using SalifyCRM.CustomerManagement.Application.Responses.Customers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalifyCRM.CustomerManagement.Application.Handlers.Customers.Queries
{
    public class GetCustomersQuery : IRequest<IDataResult<List<CustomerResponse>>>
    {
        public class GetCustomersQueryHandler : IRequestHandler<GetCustomersQuery, IDataResult<List<CustomerResponse>>>
        {
            private readonly ICustomerRepository _customerRepository;
            private readonly IMediator _mediator;

            public GetCustomersQueryHandler(
                ICustomerRepository customerRepository,
                IMediator mediator)
            {
                this._customerRepository = customerRepository;
                this._mediator = mediator;
            }

            public async Task<IDataResult<List<CustomerResponse>>> Handle(GetCustomersQuery request, CancellationToken cancellationToken)
            {
                var customers = await _customerRepository.GetListAsync(O => O.IsDeleted == false);

                List<CustomerResponse> customerResponses = CustomerExtensions.ToCustomerResponseList(customers.ToList());

                return new SuccessDataResult<List<CustomerResponse>>(customerResponses);
            }
        }
    }
}
