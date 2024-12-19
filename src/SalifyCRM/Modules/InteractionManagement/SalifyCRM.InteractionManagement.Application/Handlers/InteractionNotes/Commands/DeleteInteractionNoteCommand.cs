using Core.Utilities.Messages;
using Core.Utilities.Results;
using MediatR;
using SalifyCRM.InteractionManagement.Application.RepositoryInterfaces;
using SalifyCRM.InteractionManagement.Application.Responses.InteractionNotes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalifyCRM.InteractionManagement.Application.Handlers.InteractionNotes.Commands
{
    public class DeleteInteractionNoteCommand : IRequest<IResult>
    {
        public int DeletedUserId { get; set; }
        public int InteractionNoteId { get; set; }

        public class DeleteInteractionNoteCommandHandler : IRequestHandler<DeleteInteractionNoteCommand, IResult>
        {
            private readonly IInteractionNoteRepository _interactionNoteRepository;
            private readonly IUserRepository _userRepository;
            private readonly IMediator _mediator;

            public DeleteInteractionNoteCommandHandler(
                IInteractionNoteRepository interactionNoteRepository,
                IUserRepository userRepository,
                IMediator mediator)
            {
                this._interactionNoteRepository = interactionNoteRepository;
                this._userRepository = userRepository;
                this._mediator = mediator;
            }

            public async Task<IResult> Handle(DeleteInteractionNoteCommand request, CancellationToken cancellationToken)
            {
                if (request.InteractionNoteId <= 0)
                {
                    return new ErrorResult()
                        .AddErrorDetail(new ErrorDetail
                        {
                            Code = ErrorCode.VAL1002,
                            Message = "The provided value should not be null or negative.",
                            Title = "Null or Negative Value Error"
                        }
                        .AddMetadata("FieldName", "InteractionNoteId")
                        .AddMetadata("InputValue", request.InteractionNoteId.ToString())
                        );
                }

                if (request.DeletedUserId <= 0)
                {
                    return new ErrorResult()
                        .AddErrorDetail(new ErrorDetail
                        {
                            Code = ErrorCode.VAL1002,
                            Message = "The provided value should not be null or negative.",
                            Title = "Null or Negative Value Error"
                        }
                        .AddMetadata("FieldName", "DeletedUserId")
                        .AddMetadata("InputValue", request.DeletedUserId.ToString())
                        );
                }

                var isThereUserRecord = _userRepository.Query().Any(O => O.Id == request.DeletedUserId && O.IsDeleted == false);
                if (!isThereUserRecord)
                {
                    return new ErrorResult()
                        .AddErrorDetail(new ErrorDetail
                        {
                            Code = ErrorCode.BUS4002,
                            Message = "The requested data could not be found.",
                            Title = "User Not Found Error"
                        }
                        .AddMetadata("FieldName", "DeletedUserId")
                        .AddMetadata("InputValue", request.DeletedUserId.ToString())
                        .AddMetadata("Suggestion", "A deleted or non-existing record was requested.")
                        );
                }

                var interactionNote = await _interactionNoteRepository.GetAsync(O => O.Id == request.InteractionNoteId && O.IsDeleted == false);
                if (interactionNote == null)
                {
                    return new ErrorResult()
                        .AddErrorDetail(new ErrorDetail
                        {
                            Code = ErrorCode.BUS4002,
                            Message = "The requested data could not be found.",
                            Title = "Data Not Found Error"
                        }
                        .AddMetadata("FieldName", "InteractionNoteId")
                        .AddMetadata("InputValue", request.InteractionNoteId.ToString())
                        .AddMetadata("Suggestion", "A deleted or non-existing record was requested.")
                        );
                }

                interactionNote.IsDeleted = true;
                interactionNote.LastUpdatedDate = DateTime.UtcNow;
                interactionNote.LastUpdatedUserId = request.DeletedUserId;

                _interactionNoteRepository.Update(interactionNote);
                return new SuccessResult();
            }
        }
    }
}
