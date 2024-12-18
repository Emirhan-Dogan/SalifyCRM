using Core.Utilities.Results;
using MediatR;
using SalifyCRM.CustomerManagement.Application.Extensions;
using SalifyCRM.CustomerManagement.Application.RepositoryInterfaces;
using SalifyCRM.CustomerManagement.Application.Responses.CustomerTags;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalifyCRM.CustomerManagement.Application.Handlers.CustomerTags.Queries
{
    public class GetCustomerTagsQuery : IRequest<IDataResult<List<CustomerTagResponse>>>
    {
        public class GetCustomerTagsQueryHandler : IRequestHandler<GetCustomerTagsQuery, IDataResult<List<CustomerTagResponse>>>
        {
            private readonly ICustomerTagRepository _customerTagRepository;
            private readonly IMediator _mediator;

            public GetCustomerTagsQueryHandler(ICustomerTagRepository customerTagRepository, IMediator mediator)
            {
                this._customerTagRepository = customerTagRepository;
                this._mediator = mediator;
            }

            public async Task<IDataResult<List<CustomerTagResponse>>> Handle(GetCustomerTagsQuery request, CancellationToken cancellationToken)
            {
                var customerTags = await _customerTagRepository.GetListAsync(O => O.IsDeleted == false);

                List<CustomerTagResponse> result = CustomerTagExtensions.ToCustomerTagResponseList(customerTags.ToList());

                return new SuccessDataResult<List<CustomerTagResponse>>(result);
            }
        }
    }
}
