using Core.Utilities.Results;
using MediatR;
using SalifyCRM.CustomerManagement.Application.Extensions;
using SalifyCRM.CustomerManagement.Application.RepositoryInterfaces;
using SalifyCRM.CustomerManagement.Application.Responses.CustomerCategories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalifyCRM.CustomerManagement.Application.Handlers.CustomerCategories.Queries
{
    public class GetCustomerCategoriesQuery : IRequest<IDataResult<List<CustomerCategoryResponse>>>
    {

        public class GetCustomerCategoriesQueryHandler : IRequestHandler<GetCustomerCategoriesQuery, IDataResult<List<CustomerCategoryResponse>>>
        {
            private readonly ICustomerCategoryRepository _customerCategoryRepository;
            private readonly IMediator _mediator;

            public GetCustomerCategoriesQueryHandler(ICustomerCategoryRepository customerCategoryRepository, IMediator mediator)
            {
                this._customerCategoryRepository = customerCategoryRepository;
                this._mediator = mediator;
            }

            public async Task<IDataResult<List<CustomerCategoryResponse>>> Handle(GetCustomerCategoriesQuery request, CancellationToken cancellationToken)
            {
                var customerCategories = await _customerCategoryRepository.GetListAsync(O => O.IsDeleted == false);


                return new SuccessDataResult<List<CustomerCategoryResponse>>(
                    CustomerCategoryExtensions.ToCustomerCategoryResponseList(customerCategories.ToList())
                );
            }
        }
    }
}
