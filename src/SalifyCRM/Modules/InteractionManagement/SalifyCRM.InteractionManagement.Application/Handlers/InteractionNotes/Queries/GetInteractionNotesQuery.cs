using Core.Utilities.Results;
using MediatR;
using SalifyCRM.InteractionManagement.Application.Extensions;
using SalifyCRM.InteractionManagement.Application.RepositoryInterfaces;
using SalifyCRM.InteractionManagement.Application.Responses.InteractionNotes;
using SalifyCRM.InteractionManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalifyCRM.InteractionManagement.Application.Handlers.InteractionNotes.Queries
{
    public class GetInteractionNotesQuery : IRequest<IDataResult<List<InteractionNoteResponse>>>
    {

        public class GetInteractionNotesQueryHandler : IRequestHandler<GetInteractionNotesQuery, IDataResult<List<InteractionNoteResponse>>>
        {
            private readonly IInteractionNoteRepository _interactionNoteRepository;
            private readonly IMediator _mediator;

            public GetInteractionNotesQueryHandler(
                IInteractionNoteRepository interactionNoteRepository,
                IMediator mediator)
            {
                this._interactionNoteRepository = interactionNoteRepository;
                this._mediator = mediator;
            }

            public async Task<IDataResult<List<InteractionNoteResponse>>> Handle(GetInteractionNotesQuery request, CancellationToken cancellationToken)
            {
                var interactionNotes = await _interactionNoteRepository.GetListAsync(O => O.IsDeleted == false);
                List<InteractionNoteResponse> result = InteractionNoteExtensions.ToInteractionNoteResponseList(interactionNotes.ToList());

                return new SuccessDataResult<List<InteractionNoteResponse>>(result);
            }
        }
    }
}
