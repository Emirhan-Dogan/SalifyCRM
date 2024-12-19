using Core.Utilities.Results;
using MediatR;
using SalifyCRM.InteractionManagement.Application.Extensions;
using SalifyCRM.InteractionManagement.Application.RepositoryInterfaces;
using SalifyCRM.InteractionManagement.Application.Responses.InteractionTypes;
using SalifyCRM.InteractionManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalifyCRM.InteractionManagement.Application.Handlers.InteractionTypes.Queries
{
    public class GetInteractionTypesQuery : IRequest<IDataResult<List<InteractionTypeResponse>>>
    {

        public class GetInteractionTypesQueryHandler : IRequestHandler<GetInteractionTypesQuery, IDataResult<List<InteractionTypeResponse>>>
        {
            private readonly IInteractionTypeRepository _interactionTypeRepository;
            private readonly IMediator _mediator;

            public GetInteractionTypesQueryHandler(
                IInteractionTypeRepository interactionTypeRepository,
                IMediator mediator)
            {
                this._interactionTypeRepository = interactionTypeRepository;
                this._mediator = mediator;
            }

            public async Task<IDataResult<List<InteractionTypeResponse>>> Handle(GetInteractionTypesQuery request, CancellationToken cancellationToken)
            {
                var interactionTypes = await _interactionTypeRepository.GetListAsync(O => O.IsDeleted == false);
                List<InteractionTypeResponse> result = InteractionTypeExtensions.ToInteractionTypeResponseList(interactionTypes.ToList());

                return new SuccessDataResult<List<InteractionTypeResponse>>(result);
            }
        }
    }
}
