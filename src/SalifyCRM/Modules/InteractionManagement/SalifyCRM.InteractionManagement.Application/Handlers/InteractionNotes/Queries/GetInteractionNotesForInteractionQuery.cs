using Core.Utilities.Messages;
using Core.Utilities.Results;
using MediatR;
using SalifyCRM.InteractionManagement.Application.Extensions;
using SalifyCRM.InteractionManagement.Application.RepositoryInterfaces;
using SalifyCRM.InteractionManagement.Application.Responses.InteractionNotes;
using SalifyCRM.InteractionManagement.Application.Responses.Interactions;
using SalifyCRM.InteractionManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalifyCRM.InteractionManagement.Application.Handlers.InteractionNotes.Queries
{
    public class GetInteractionNotesForInteractionQuery : IRequest<IDataResult<List<InteractionNoteResponse>>>
    {
        public int InteractionId { get; set; }

        public class GetInteractionNotesForInteractionQueryHandler : IRequestHandler<GetInteractionNotesForInteractionQuery, IDataResult<List<InteractionNoteResponse>>>
        {
            private readonly IInteractionNoteRepository _interactionNoteRepository;
            private readonly IInteractionRepository _interactionRepository;
            private readonly IMediator _mediator;

            public GetInteractionNotesForInteractionQueryHandler(
                IInteractionNoteRepository interactionNoteRepository,
                IInteractionRepository interactionRepository,
                IMediator mediator)
            {
                this._interactionNoteRepository = interactionNoteRepository;
                this._interactionRepository = interactionRepository;
                this._mediator = mediator;
            }

            public async Task<IDataResult<List<InteractionNoteResponse>>> Handle(GetInteractionNotesForInteractionQuery request, CancellationToken cancellationToken)
            {
                if (request.InteractionId <= 0)
                {
                    return new ErrorDataResult<List<InteractionNoteResponse>>()
                        .AddErrorDetail(new ErrorDetail
                        {
                            Code = ErrorCode.VAL1002,
                            Message = "The provided value should not be null or negative.",
                            Title = "Null or Negative Value Error"
                        }
                        .AddMetadata("FieldName", "InteractionId")
                        .AddMetadata("InputValue", request.InteractionId.ToString())
                        );
                }

                var isThereInteractionRecord = _interactionRepository.Query().Any(O => O.Id == request.InteractionId && O.IsDeleted == false);
                if (!isThereInteractionRecord)
                {
                    return new ErrorDataResult<List<InteractionNoteResponse>>()
                        .AddErrorDetail(new ErrorDetail
                        {
                            Code = ErrorCode.BUS4002,
                            Message = "The requested data could not be found.",
                            Title = "Data Not Found Error"
                        }
                        .AddMetadata("FieldName", "InteractionId")
                        .AddMetadata("InputValue", request.InteractionId.ToString())
                        .AddMetadata("Suggestion", "A deleted or non-existing record was requested.")
                        );
                }

                var interactionNotes = await _interactionNoteRepository.GetListAsync(O => O.InteractionId == request.InteractionId && O.IsDeleted == false);
                List<InteractionNoteResponse> result = InteractionNoteExtensions.ToInteractionNoteResponseList(interactionNotes.ToList());

                return new SuccessDataResult<List<InteractionNoteResponse>>(result);
            }
        }
    }
}
