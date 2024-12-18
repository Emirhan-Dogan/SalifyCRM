using Core.Utilities.Results;
using MediatR;
using SalifyCRM.CustomerManagement.Application.Extensions;
using SalifyCRM.CustomerManagement.Application.RepositoryInterfaces;
using SalifyCRM.CustomerManagement.Application.Responses.Cities;
using SalifyCRM.CustomerManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalifyCRM.CustomerManagement.Application.Handlers.Cities.Queries
{
    public class GetCitiesQuery : IRequest<IDataResult<List<CityResponse>>>
    {
        public class GetCitiesQueryHandler : IRequestHandler<GetCitiesQuery, IDataResult<List<CityResponse>>>
        {
            private readonly ICityRepository _cityRepository;
            private readonly IMediator _mediator;

            public GetCitiesQueryHandler(
                ICityRepository cityRepository,
                IMediator mediator)
            {
                this._cityRepository = cityRepository;
                this._mediator = mediator;
            }

            public async Task<IDataResult<List<CityResponse>>> Handle(GetCitiesQuery request, CancellationToken cancellationToken)
            {
                var cities = await _cityRepository.GetListAsync(O => O.IsDeleted == false);
                List<CityResponse> cityResponses = CityExtensions.ToCityResponseList(cities.ToList());

                return new SuccessDataResult<List<CityResponse>>(cityResponses);
            }
        }
    }
}
