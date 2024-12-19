using Core.Utilities.Results;
using MediatR;
using SalifyCRM.InteractionManagement.Application.Extensions;
using SalifyCRM.InteractionManagement.Application.RepositoryInterfaces;
using SalifyCRM.InteractionManagement.Application.Responses.Interactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalifyCRM.InteractionManagement.Application.Handlers.Interactions.Queries
{
    public class GetInteractionsQuery : IRequest<IDataResult<List<InteractionResponse>>>
    {

        public class GetInteractionsQueryHandler : IRequestHandler<GetInteractionsQuery, IDataResult<List<InteractionResponse>>>
        {
            private readonly IInteractionRepository _interactionRepository;
            private readonly IMediator _mediator;

            public GetInteractionsQueryHandler(
                IInteractionRepository interactionRepository,
                IMediator mediator)
            {
                this._interactionRepository = interactionRepository;
                this._mediator = mediator;
            }

            public async Task<IDataResult<List<InteractionResponse>>> Handle(GetInteractionsQuery request, CancellationToken cancellationToken)
            {
                var interactions = await _interactionRepository.GetListAsync(O => O.IsDeleted == false);

                List<InteractionResponse> interactionResponses = InteractionExtensions.ToInteractionResponseList(interactions.ToList());

                return new SuccessDataResult<List<InteractionResponse>>(interactionResponses);
            }
        }
    }
}
