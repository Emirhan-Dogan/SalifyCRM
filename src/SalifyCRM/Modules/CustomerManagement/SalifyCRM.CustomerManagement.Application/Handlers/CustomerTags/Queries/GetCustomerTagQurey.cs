using Core.Utilities.Messages;
using Core.Utilities.Results;
using MediatR;
using SalifyCRM.CustomerManagement.Application.Extensions;
using SalifyCRM.CustomerManagement.Application.RepositoryInterfaces;
using SalifyCRM.CustomerManagement.Application.Responses.Customers;
using SalifyCRM.CustomerManagement.Application.Responses.CustomerTags;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalifyCRM.CustomerManagement.Application.Handlers.CustomerTags.Queries
{
    public class GetCustomerTagQuery : IRequest<IDataResult<CustomerTagDetailResponse>>
    {
        public int Id { get; set; }

        public class GetCustomerTagQueryHandler : IRequestHandler<GetCustomerTagQuery, IDataResult<CustomerTagDetailResponse>>
        {
            private readonly ICustomerTagRepository _customerTagRepository;
            private readonly ICustomerRepository _customerRepository;
            private readonly IMediator _mediator;

            public GetCustomerTagQueryHandler(ICustomerTagRepository customerTagRepository, ICustomerRepository customerRepository, IMediator mediator)
            {
                this._customerTagRepository = customerTagRepository;
                this._customerRepository = customerRepository;
                this._mediator = mediator;
            }

            public async Task<IDataResult<CustomerTagDetailResponse>> Handle(GetCustomerTagQuery request, CancellationToken cancellationToken)
            {
                if (request.Id == 0)
                {
                    return new ErrorDataResult<CustomerTagDetailResponse>()
                        .AddErrorDetail(new ErrorDetail
                        {
                            Code = ErrorCode.VAL1001,
                            Message = "Required field is missing.",
                            Title = "Missing Field Error"
                        }
                        .AddMetadata("FieldName", "Id")
                        .AddMetadata("InputValue", "Null")
                        );
                }
                if (request.Id < 0)
                {
                    return new ErrorDataResult<CustomerTagDetailResponse>()
                        .AddErrorDetail(new ErrorDetail
                        {
                            Code = ErrorCode.VAL1002,
                            Message = "The provided value should not be negative.",
                            Title = "Negative Value Error"
                        }
                        .AddMetadata("FieldName", "Id")
                        .AddMetadata("InputValue", request.Id.ToString())
                        );
                }

                var customerTag = await _customerTagRepository.GetAsync(O => O.Id == request.Id && O.IsDeleted == false);
                if (customerTag == null)
                {
                    return new ErrorDataResult<CustomerTagDetailResponse>()
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

                var customers = await _customerRepository.GetListAsync(O=>O.Id == request.Id);
                List<CustomerResponse> customerResponses = CustomerExtensions.ToCustomerResponseList(customers.ToList());

                CustomerTagDetailResponse result = CustomerTagExtensions.ToCustomerTagDetailResponse(customerTag, customerResponses);

                return new SuccessDataResult<CustomerTagDetailResponse>(result);
            }
        }
    }
}
