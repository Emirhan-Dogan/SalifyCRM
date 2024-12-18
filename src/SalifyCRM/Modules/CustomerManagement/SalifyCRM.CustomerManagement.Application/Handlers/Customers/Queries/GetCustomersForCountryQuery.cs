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
    public class GetCustomersForCountryQuery : IRequest<IDataResult<List<CustomerResponse>>>
    {
        public int CountryId { get; set; }

        public class GetCustomersForCountryQueryHandler : IRequestHandler<GetCustomersForCountryQuery, IDataResult<List<CustomerResponse>>>
        {
            private readonly ICustomerRepository _customerRepository;
            private readonly ICountryRepository _countryRepository;
            private readonly IMediator _mediator;

            public GetCustomersForCountryQueryHandler(
                ICustomerRepository customerRepository,
                ICountryRepository countryRepository,
                IMediator mediator)
            {
                this._customerRepository = customerRepository;
                this._countryRepository = countryRepository;
                this._mediator = mediator;
            }

            public async Task<IDataResult<List<CustomerResponse>>> Handle(GetCustomersForCountryQuery request, CancellationToken cancellationToken)
            {
                if (request.CountryId <= 0)
                {
                    return new ErrorDataResult<List<CustomerResponse>>()
                        .AddErrorDetail(new ErrorDetail
                        {
                            Code = ErrorCode.VAL1002,
                            Message = "The provided value should not be null or negative.",
                            Title = "Null or Negative Value Error"
                        }
                        .AddMetadata("FieldName", "CountryId")
                        .AddMetadata("InputValue", request.CountryId.ToString())
                        );
                }

                var isThereCountryRecord = _countryRepository.Query().Any(u => u.Id == request.CountryId);
                if (!isThereCountryRecord)
                {
                    return new ErrorDataResult<List<CustomerResponse>>()
                         .AddErrorDetail(new ErrorDetail
                         {
                             Code = ErrorCode.BUS4003,
                             Message = "No country was found with the provided ID.",
                             Title = "Country Not Found"
                         }
                        .AddMetadata("FieldName", "CountryId")
                        .AddMetadata("InputValue", request.CountryId.ToString())
                        );
                }

                var customers = _customerRepository.GetCustomersByCountryId(request.CountryId);
                List<CustomerResponse> customerResponses = CustomerExtensions.ToCustomerResponseList(customers.ToList());

                return new SuccessDataResult<List<CustomerResponse>>(customerResponses);
            }
        }
    }
}
