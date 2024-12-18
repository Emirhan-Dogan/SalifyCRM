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
    public class GetCustomersForCityQuery : IRequest<IDataResult<List<CustomerResponse>>>
    {
        public int CityId { get; set; }

        public class GetCustomersForCityQueryHandler : IRequestHandler<GetCustomersForCityQuery, IDataResult<List<CustomerResponse>>>
        {
            private readonly ICustomerRepository _customerRepository;
            private readonly ICityRepository _cityRepository;
            private readonly IMediator _mediator;

            public GetCustomersForCityQueryHandler(
                ICustomerRepository customerRepository,
                ICityRepository cityRepository,
                IMediator mediator)
            {
                this._customerRepository = customerRepository;
                this._cityRepository = cityRepository;
                this._mediator = mediator;
            }

            public async Task<IDataResult<List<CustomerResponse>>> Handle(GetCustomersForCityQuery request, CancellationToken cancellationToken)
            {
                if (request.CityId <= 0)
                {
                    return new ErrorDataResult<List<CustomerResponse>> ()
                        .AddErrorDetail(new ErrorDetail
                        {
                            Code = ErrorCode.VAL1002,
                            Message = "The provided value should not be null or negative.",
                            Title = "Null or Negative Value Error"
                        }
                        .AddMetadata("FieldName", "CityId")
                        .AddMetadata("InputValue", request.CityId.ToString())
                        );
                }

                var isThereCityRecord = _cityRepository.Query().Any(u => u.Id == request.CityId);
                if (!isThereCityRecord)
                {
                    return new ErrorDataResult<List<CustomerResponse>>()
                         .AddErrorDetail(new ErrorDetail
                         {
                             Code = ErrorCode.BUS4003,
                             Message = "No city was found with the provided ID.",
                             Title = "City Not Found"
                         }
                        .AddMetadata("FieldName", "CityId")
                        .AddMetadata("InputValue", request.CityId.ToString())
                        );
                }

                var customers = _customerRepository.GetCustomersByCityId(request.CityId);
                List<CustomerResponse> customerResponses = CustomerExtensions.ToCustomerResponseList(customers.ToList());

                return new SuccessDataResult<List<CustomerResponse>>(customerResponses);
            }
        }
    }
}
