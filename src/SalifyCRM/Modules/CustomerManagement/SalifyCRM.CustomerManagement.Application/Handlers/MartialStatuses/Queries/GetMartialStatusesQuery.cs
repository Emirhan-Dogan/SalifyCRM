using Core.Utilities.Results;
using MediatR;
using SalifyCRM.CustomerManagement.Application.Extensions;
using SalifyCRM.CustomerManagement.Application.RepositoryInterfaces;
using SalifyCRM.CustomerManagement.Application.Responses.MartialStatuses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalifyCRM.CustomerManagement.Application.Handlers.MartialStatuses.Queries
{
    public class GetMartialStatusesQuery : IRequest<IDataResult<List<MartialStatusResponse>>>
    {
        public class GetMartialStatusesQueryHandler : IRequestHandler<GetMartialStatusesQuery, IDataResult<List<MartialStatusResponse>>>
        {
            private readonly IMartialStatusRepository _martialStatusRepository;
            private readonly IMediator _mediator;

            public GetMartialStatusesQueryHandler(IMartialStatusRepository martialStatusRepository, IMediator mediator)
            {
                this._martialStatusRepository = martialStatusRepository;
                this._mediator = mediator;
            }

            public async Task<IDataResult<List<MartialStatusResponse>>> Handle(GetMartialStatusesQuery request, CancellationToken cancellationToken)
            {
                var martialStatuses = await _martialStatusRepository.GetListAsync(O => O.IsDeleted == false);

                List<MartialStatusResponse> result = MartialStatusExtensions.ToMartialStatusResponseList(martialStatuses.ToList());

                return new SuccessDataResult<List<MartialStatusResponse>>(result);
            }
        }
    }
}
