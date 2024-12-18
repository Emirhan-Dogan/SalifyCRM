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
    public class GetCustomersForCustomerTypeQuery : IRequest<IDataResult<List<CustomerResponse>>>
    {
        public int CustomerTypeId { get; set; }

        public class GetCustomersForCustomerTypeQueryHandler : IRequestHandler<GetCustomersForCustomerTypeQuery, IDataResult<List<CustomerResponse>>>
        {
            private readonly ICustomerRepository _customerRepository;
            private readonly ICustomerTypeRepository _customerTypeRepository;
            private readonly IMediator _mediator;

            public GetCustomersForCustomerTypeQueryHandler(
                ICustomerRepository customerRepository,
                ICustomerTypeRepository customerTypeRepository,
                IMediator mediator)
            {
                this._customerRepository = customerRepository;
                this._customerTypeRepository = customerTypeRepository;
                this._mediator = mediator;
            }

            public async Task<IDataResult<List<CustomerResponse>>> Handle(GetCustomersForCustomerTypeQuery request, CancellationToken cancellationToken)
            {
                if (request.CustomerTypeId <= 0)
                {
                    return new ErrorDataResult<List<CustomerResponse>>()
                        .AddErrorDetail(new ErrorDetail
                        {
                            Code = ErrorCode.VAL1002,
                            Message = "The provided value should not be null or negative.",
                            Title = "Null or Negative Value Error"
                        }
                        .AddMetadata("FieldName", "CustomerTypeId")
                        .AddMetadata("InputValue", request.CustomerTypeId.ToString())
                        );
                }

                var isThereCustomerTypeRecord = _customerTypeRepository.Query().Any(u => u.Id == request.CustomerTypeId);
                if (!isThereCustomerTypeRecord)
                {
                    return new ErrorDataResult<List<CustomerResponse>>()
                         .AddErrorDetail(new ErrorDetail
                         {
                             Code = ErrorCode.BUS4003,
                             Message = "No customer type was found with the provided ID.",
                             Title = "Customer Type Not Found"
                         }
                        .AddMetadata("FieldName", "CustomerTypeId")
                        .AddMetadata("InputValue", request.CustomerTypeId.ToString())
                        );
                }

                var customers = await _customerRepository.GetListAsync(O => O.CustomerTypeId == request.CustomerTypeId && O.IsDeleted == false);
                List<CustomerResponse> customerResponses = CustomerExtensions.ToCustomerResponseList(customers.ToList());

                return new SuccessDataResult<List<CustomerResponse>>(customerResponses);
            }
        }
    }
}
