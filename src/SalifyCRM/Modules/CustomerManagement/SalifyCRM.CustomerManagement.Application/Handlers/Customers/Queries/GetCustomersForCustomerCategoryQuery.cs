using Core.Utilities.Messages;
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
    public class GetCustomersForCustomerCategoryQuery : IRequest<IDataResult<List<CustomerResponse>>>
    {
        public int CustomerCategoryId { get; set; }

        public class GetCustomersForCustomerCategoryQueryHandler : IRequestHandler<GetCustomersForCustomerCategoryQuery, IDataResult<List<CustomerResponse>>>
        {
            private readonly ICustomerRepository _customerRepository;
            private readonly ICustomerCategoryRepository _customerCategoryRepository;
            private readonly IMediator _mediator;

            public GetCustomersForCustomerCategoryQueryHandler(
                ICustomerRepository customerRepository,
                ICustomerCategoryRepository customerCategoryRepository,
                IMediator mediator)
            {
                this._customerRepository = customerRepository;
                this._customerCategoryRepository = customerCategoryRepository;
                this._mediator = mediator;
            }

            public async Task<IDataResult<List<CustomerResponse>>> Handle(GetCustomersForCustomerCategoryQuery request, CancellationToken cancellationToken)
            {
                if (request.CustomerCategoryId <= 0)
                {
                    return new ErrorDataResult<List<CustomerResponse>>()
                        .AddErrorDetail(new ErrorDetail
                        {
                            Code = ErrorCode.VAL1002,
                            Message = "The provided value should not be null or negative.",
                            Title = "Null or Negative Value Error"
                        }
                        .AddMetadata("FieldName", "CustomerCategoryId")
                        .AddMetadata("InputValue", request.CustomerCategoryId.ToString())
                        );
                }

                var isThereCustomerCategoryRecord = _customerCategoryRepository.Query().Any(u => u.Id == request.CustomerCategoryId);
                if (!isThereCustomerCategoryRecord)
                {
                    return new ErrorDataResult<List<CustomerResponse>>()
                         .AddErrorDetail(new ErrorDetail
                         {
                             Code = ErrorCode.BUS4003,
                             Message = "No customer category was found with the provided ID.",
                             Title = "Customer Category Not Found"
                         }
                        .AddMetadata("FieldName", "CustomerCategoryId")
                        .AddMetadata("InputValue", request.CustomerCategoryId.ToString())
                        );
                }

                var customers = await _customerRepository.GetListAsync(O => O.CustomerCategoryId == request.CustomerCategoryId && O.IsDeleted == false);
                List<CustomerResponse> customerResponses = CustomerExtensions.ToCustomerResponseList(customers.ToList());

                return new SuccessDataResult<List<CustomerResponse>>(customerResponses);
            }
        }
    }
}
