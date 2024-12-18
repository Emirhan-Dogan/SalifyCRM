using Core.Utilities.Messages;
using Core.Utilities.Results;
using MediatR;
using SalifyCRM.CustomerManagement.Application.RepositoryInterfaces;
using SalifyCRM.CustomerManagement.Application.Responses.Countries;
using SalifyCRM.CustomerManagement.Application.Responses.CustomerTagMappings;
using SalifyCRM.CustomerManagement.Application.Responses.Occupations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalifyCRM.CustomerManagement.Application.Handlers.Countries.Queries
{
    public class GetCountryQuery : IRequest<IDataResult<CountryDetailResponse>>
    {
        public int Id { get; set; }

        public class GetCountryQueryHandler : IRequestHandler<GetCountryQuery, IDataResult<CountryDetailResponse>>
        {
            private readonly ICountryRepository _countryRepository; // method 
            private readonly IMediator _mediator;

            public GetCountryQueryHandler(
                ICountryRepository countryRepository,
                IMediator mediator)
            {
                this._countryRepository = countryRepository;
                this._mediator = mediator;
            }

            public async Task<IDataResult<CountryDetailResponse>> Handle(GetCountryQuery request, CancellationToken cancellationToken)
            {
                if (request.Id <= 0)
                {
                    return new ErrorDataResult<CountryDetailResponse>()
                        .AddErrorDetail(new ErrorDetail
                        {
                            Code = ErrorCode.VAL1002,
                            Message = "The provided value should not be null or negative.",
                            Title = "Null or Negative Value Error"
                        }
                        .AddMetadata("FieldName", "Id")
                        .AddMetadata("InputValue", request.Id.ToString())
                        );
                }

                var country = _countryRepository.GetCountryWithCitiesAndDistricts(request.Id);
                if (country == null)
                {
                    return new ErrorDataResult<CountryDetailResponse>()
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

                return new SuccessDataResult<CountryDetailResponse>(country);
            }
        }
    }
}
