using Core.Utilities.Results;
using MediatR;
using SalifyCRM.CustomerManagement.Application.Extensions;
using SalifyCRM.CustomerManagement.Application.RepositoryInterfaces;
using SalifyCRM.CustomerManagement.Application.Responses.Occupations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalifyCRM.CustomerManagement.Application.Handlers.Occupations.Queries
{
    public class GetOccupationsQuery : IRequest<IDataResult<List<OccupationResponse>>>
    {
        public class GetOccupationsQueryHandler : IRequestHandler<GetOccupationsQuery, IDataResult<List<OccupationResponse>>>
        {
            private readonly IOccupationRepository _occupationRepository;
            private readonly IMediator _mediator;

            public GetOccupationsQueryHandler(IOccupationRepository occupationRepository, IMediator mediator)
            {
                this._occupationRepository = occupationRepository;
                this._mediator = mediator;
            }

            public async Task<IDataResult<List<OccupationResponse>>> Handle(GetOccupationsQuery request, CancellationToken cancellationToken)
            {
                var occupations = await _occupationRepository.GetListAsync(O => O.IsDeleted == false);

                List<OccupationResponse> result = OccupationExtensions.ToOccupationResponseList(occupations.ToList());

                return new SuccessDataResult<List<OccupationResponse>>(result);
            }
        }
    }
}
