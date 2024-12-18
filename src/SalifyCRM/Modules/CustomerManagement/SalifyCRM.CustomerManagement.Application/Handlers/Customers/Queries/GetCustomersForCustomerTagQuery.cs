using Core.Utilities.Messages;
using Core.Utilities.Results;
using MediatR;
using SalifyCRM.CustomerManagement.Application.Extensions;
using SalifyCRM.CustomerManagement.Application.RepositoryInterfaces;
using SalifyCRM.CustomerManagement.Application.Responses.Customers;
using SalifyCRM.CustomerManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalifyCRM.CustomerManagement.Application.Handlers.Customers.Queries
{
    public class GetCustomersForCustomerTagQuery : IRequest<IDataResult<List<CustomerResponse>>>
    {
        public int CustomerTagId { get; set; }

        public class GetCustomersForCustomerTagQueryHandler : IRequestHandler<GetCustomersForCustomerTagQuery, IDataResult<List<CustomerResponse>>>
        {
            private readonly ICustomerRepository _customerRepository;
            private readonly ICustomerTagRepository _customerTagRepository;
            private readonly IMediator _mediator;

            public GetCustomersForCustomerTagQueryHandler(
                ICustomerRepository customerRepository,
                ICustomerTagRepository customerTagRepository,
                IMediator mediator)
            {
                this._customerRepository = customerRepository;
                this._customerTagRepository = customerTagRepository;
                this._mediator = mediator;
            }

            public async Task<IDataResult<List<CustomerResponse>>> Handle(GetCustomersForCustomerTagQuery request, CancellationToken cancellationToken)
            {
                if (request.CustomerTagId <= 0)
                {
                    return new ErrorDataResult<List<CustomerResponse>>()
                        .AddErrorDetail(new ErrorDetail
                        {
                            Code = ErrorCode.VAL1002,
                            Message = "The provided value should not be null or negative.",
                            Title = "Null or Negative Value Error"
                        }
                        .AddMetadata("FieldName", "CustomerTagId")
                        .AddMetadata("InputValue", request.CustomerTagId.ToString())
                        );
                }

                var isThereCustomerTagRecord = _customerTagRepository.Query().Any(u => u.Id == request.CustomerTagId);
                if (!isThereCustomerTagRecord)
                {
                    return new ErrorDataResult<List<CustomerResponse>>()
                         .AddErrorDetail(new ErrorDetail
                         {
                             Code = ErrorCode.BUS4003,
                             Message = "No customer tag was found with the provided ID.",
                             Title = "Customer Tag Not Found"
                         }
                        .AddMetadata("FieldName", "CustomerTagId")
                        .AddMetadata("InputValue", request.CustomerTagId.ToString())
                        );
                }

                var customers = _customerRepository.GetCustomersByCustomerTagId(request.CustomerTagId);
                List<CustomerResponse> customerResponses = CustomerExtensions.ToCustomerResponseList(customers.ToList());

                return new SuccessDataResult<List<CustomerResponse>>(customerResponses);
            }
        }
    }
}
