using Core.Utilities.Results;
using MediatR;
using SalifyCRM.CustomerManagement.Application.Extensions;
using SalifyCRM.CustomerManagement.Application.RepositoryInterfaces;
using SalifyCRM.CustomerManagement.Application.Responses.Countries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalifyCRM.CustomerManagement.Application.Handlers.Countries.Queries
{
    public class GetCountriesQuery : IRequest<IDataResult<List<CountryResponse>>>
    {
        public class GetCountriesQueryHandler : IRequestHandler<GetCountriesQuery, IDataResult<List<CountryResponse>>>
        {
            private readonly ICountryRepository _countryRepository;
            private readonly IMediator _mediator;

            public GetCountriesQueryHandler(
                ICountryRepository countryRepository,
                IMediator mediator)
            {
                this._countryRepository = countryRepository;
                this._mediator = mediator;
            }

            public async Task<IDataResult<List<CountryResponse>>> Handle(GetCountriesQuery request, CancellationToken cancellationToken)
            {
                var country = await _countryRepository.GetListAsync(O => O.IsDeleted == false);
                List<CountryResponse> countryResponses = CountryExtensions.ToCountryResponseList(country.ToList());

                return new SuccessDataResult<List<CountryResponse>>(countryResponses);
            }
        }
    }
}
