using Core.Utilities.Messages;
using Core.Utilities.Results;
using MediatR;
using SalifyCRM.InteractionManagement.Application.Extensions;
using SalifyCRM.InteractionManagement.Application.RepositoryInterfaces;
using SalifyCRM.InteractionManagement.Application.Responses.InteractionNotes;
using SalifyCRM.InteractionManagement.Application.Responses.Interactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalifyCRM.InteractionManagement.Application.Handlers.Interactions.Queries
{
    public class GetInteractionQuery : IRequest<IDataResult<InteractionDetailResponse>>
    {
        public int Id { get; set; }

        public class GetInteractionQueryHandler : IRequestHandler<GetInteractionQuery, IDataResult<InteractionDetailResponse>>
        {
            private readonly IInteractionRepository _interactionRepository;
            private readonly IInteractionNoteRepository _interactionNoteRepository;
            private readonly IMediator _mediator;

            public GetInteractionQueryHandler(
                IInteractionRepository interactionRepository,
                IInteractionNoteRepository interactionNoteRepository,
                IMediator mediator)
            {
                this._interactionRepository = interactionRepository;
                this._interactionNoteRepository = interactionNoteRepository;
                this._mediator = mediator;
            }

            public async Task<IDataResult<InteractionDetailResponse>> Handle(GetInteractionQuery request, CancellationToken cancellationToken)
            {
                if (request.Id <= 0)
                {
                    return new ErrorDataResult<InteractionDetailResponse>()
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

                var interaction = await _interactionRepository.GetAsync(O => O.Id == request.Id && O.IsDeleted == false);
                if(interaction == null)
                {
                    return new ErrorDataResult<InteractionDetailResponse>()
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

                var interactionNote = await _interactionNoteRepository.GetListAsync(O => O.InteractionId == request.Id && O.IsDeleted == false);
                InteractionDetailResponse result = InteractionExtensions.ToInteractionDetailResponse(interaction, interactionNote.ToList());

                return new SuccessDataResult<InteractionDetailResponse>(result);
            }
        }
    }
}
