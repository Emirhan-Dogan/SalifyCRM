using Core.Utilities.Messages;
using Core.Utilities.Results;
using MediatR;
using SalifyCRM.InteractionManagement.Application.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalifyCRM.InteractionManagement.Application.Handlers.InteractionNotes.Commands
{
    public class UpdateInteractionNoteCommand : IRequest<IResult>
    {
        public int LastUpdatedUserId { get; set; }

        public int Id { get; set; }
        public string Note { get; set; }
        public DateTime NoteDate { get; set; }
        public string Status { get; set; }

        public class UpdateInteractionNoteCommandHandler : IRequestHandler<UpdateInteractionNoteCommand, IResult>
        {
            private readonly IInteractionNoteRepository _interactionNoteRepository;
            private readonly IUserRepository _userRepository;
            private readonly IMediator _mediator;

            public UpdateInteractionNoteCommandHandler(
                IInteractionNoteRepository interactionNoteRepository,
                IUserRepository userRepository,
                IMediator mediator)
            {
                this._interactionNoteRepository = interactionNoteRepository;
                this._userRepository = userRepository;
                this._mediator = mediator;
            }

            public async Task<IResult> Handle(UpdateInteractionNoteCommand request, CancellationToken cancellationToken)
            {
                if (request.Id <= 0)
                {
                    return new ErrorResult()
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

                if (request.LastUpdatedUserId <= 0)
                {
                    return new ErrorResult()
                        .AddErrorDetail(new ErrorDetail
                        {
                            Code = ErrorCode.VAL1002,
                            Message = "The provided value should not be null or negative.",
                            Title = "Null or Negative Value Error"
                        }
                        .AddMetadata("FieldName", "LastUpdatedUserId")
                        .AddMetadata("InputValue", request.LastUpdatedUserId.ToString())
                        );
                }

                var isThereUserRecord = _userRepository.Query().Any(O => O.Id == request.LastUpdatedUserId && O.IsDeleted == false);
                if (!isThereUserRecord)
                {
                    return new ErrorResult()
                        .AddErrorDetail(new ErrorDetail
                        {
                            Code = ErrorCode.BUS4002,
                            Message = "The requested user could not be found.",
                            Title = "User Not Found Error"
                        }
                        .AddMetadata("FieldName", "LastUpdatedUserId")
                        .AddMetadata("InputValue", request.LastUpdatedUserId.ToString())
                        .AddMetadata("Suggestion", "A deleted or non-existing record was requested.")
                        );
                }

                var interactionNote = await _interactionNoteRepository.GetAsync(O => O.Id == request.Id && O.IsDeleted == false);
                if (interactionNote == null)
                {
                    return new ErrorResult()
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

                interactionNote.Note = request.Note;
                interactionNote.Status = request.Status;
                interactionNote.NoteDate = request.NoteDate;
                interactionNote.LastUpdatedUserId = request.LastUpdatedUserId;
                interactionNote.LastUpdatedDate = DateTime.UtcNow;

                _interactionNoteRepository.Update(interactionNote);
                return new SuccessResult();
            }
        }
    }
}
