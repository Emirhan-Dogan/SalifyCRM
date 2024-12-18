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
    public class GetCustomersForDistrictQuery : IRequest<IDataResult<List<CustomerResponse>>>
    {
        public int DistrictId { get; set; }

        public class GetCustomersForDistrictQueryHandler : IRequestHandler<GetCustomersForDistrictQuery, IDataResult<List<CustomerResponse>>>
        {
            private readonly ICustomerRepository _customerRepository;
            private readonly IDistrictRepository _districtRepository;
            private readonly IMediator _mediator;

            public GetCustomersForDistrictQueryHandler(
                ICustomerRepository customerRepository,
                IDistrictRepository districtRepository,
                IMediator mediator)
            {
                this._customerRepository = customerRepository;
                this._districtRepository = districtRepository;
                this._mediator = mediator;
            }

            public async Task<IDataResult<List<CustomerResponse>>> Handle(GetCustomersForDistrictQuery request, CancellationToken cancellationToken)
            {
                if (request.DistrictId <= 0)
                {
                    return new ErrorDataResult<List<CustomerResponse>>()
                        .AddErrorDetail(new ErrorDetail
                        {
                            Code = ErrorCode.VAL1002,
                            Message = "The provided value should not be null or negative.",
                            Title = "Null or Negative Value Error"
                        }
                        .AddMetadata("FieldName", "DistrictId")
                        .AddMetadata("InputValue", request.DistrictId.ToString())
                        );
                }

                var isThereDistrictRecord = _districtRepository.Query().Any(u => u.Id == request.DistrictId);
                if (!isThereDistrictRecord)
                {
                    return new ErrorDataResult<List<CustomerResponse>>()
                         .AddErrorDetail(new ErrorDetail
                         {
                             Code = ErrorCode.BUS4003,
                             Message = "No district was found with the provided ID.",
                             Title = "District Not Found"
                         }
                        .AddMetadata("FieldName", "DistrictId")
                        .AddMetadata("InputValue", request.DistrictId.ToString())
                        );
                }

                var customers = _customerRepository.GetCustomersByDistrictId(request.DistrictId);
                List<CustomerResponse> customerResponses = CustomerExtensions.ToCustomerResponseList(customers.ToList());

                return new SuccessDataResult<List<CustomerResponse>>(customerResponses);
            }
        }
    }
}
