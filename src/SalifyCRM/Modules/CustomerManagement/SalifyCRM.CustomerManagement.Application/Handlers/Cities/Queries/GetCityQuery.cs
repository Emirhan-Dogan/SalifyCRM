using Core.Utilities.Messages;
using Core.Utilities.Results;
using MediatR;
using SalifyCRM.CustomerManagement.Application.RepositoryInterfaces;
using SalifyCRM.CustomerManagement.Application.Responses.Cities;
using SalifyCRM.CustomerManagement.Application.Responses.Countries;
using SalifyCRM.CustomerManagement.Application.Responses.CustomerTagMappings;
using SalifyCRM.CustomerManagement.Application.Responses.Districts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalifyCRM.CustomerManagement.Application.Handlers.Cities.Queries
{
    public class GetCityQuery : IRequest<IDataResult<CityDetailResponse>>
    {
        public int Id { get; set; }

        public class GetCityQueryHandler : IRequestHandler<GetCityQuery, IDataResult<CityDetailResponse>>
        {
            private readonly ICityRepository _cityRepository; // method
            private readonly IMediator _mediator;

            public GetCityQueryHandler(
                ICityRepository cityRepository,
                IMediator mediator)
            {
                this._cityRepository = cityRepository;
                this._mediator = mediator;
            }

            public async Task<IDataResult<CityDetailResponse>> Handle(GetCityQuery request, CancellationToken cancellationToken)
            {
                if (request.Id <= 0)
                {
                    return new ErrorDataResult<CityDetailResponse>()
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

                var city = _cityRepository.GetCityWithDistricts(request.Id);
                if(city == null)
                {
                    return new ErrorDataResult<CityDetailResponse>()
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

                return new SuccessDataResult<CityDetailResponse>(city);
            }
        }
    }
}
