using Core.Utilities.Results;
using MediatR;
using SalifyCRM.CustomerManagement.Application.Extensions;
using SalifyCRM.CustomerManagement.Application.RepositoryInterfaces;
using SalifyCRM.CustomerManagement.Application.Responses.Districts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalifyCRM.CustomerManagement.Application.Handlers.Districts.Queries
{
    public class GetDistrictsQuery : IRequest<IDataResult<List<DistrictResponse>>>
    {
        public class GetDistrictsQueryHandler : IRequestHandler<GetDistrictsQuery, IDataResult<List<DistrictResponse>>>
        {
            private readonly IDistrictRepository _districtRepository;
            private readonly IMediator _mediator;

            public GetDistrictsQueryHandler(
                IDistrictRepository districtRepository,
                IMediator mediator)
            {
                this._districtRepository = districtRepository;
                this._mediator = mediator;
            }

            public async Task<IDataResult<List<DistrictResponse>>> Handle(GetDistrictsQuery request, CancellationToken cancellationToken)
            {
                var districts = await _districtRepository.GetListAsync(O => O.IsDeleted == false);

                List<DistrictResponse> districtResponses = DistrictExtensions.ToDistrictResponseList(districts.ToList());

                return new SuccessDataResult<List<DistrictResponse>>(districtResponses);
            }
        }
    }
}
