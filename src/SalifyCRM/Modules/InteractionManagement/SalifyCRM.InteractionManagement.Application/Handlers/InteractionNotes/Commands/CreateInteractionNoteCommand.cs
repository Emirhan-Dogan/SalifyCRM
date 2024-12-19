using Core.Utilities.Messages;
using Core.Utilities.Results;
using MediatR;
using SalifyCRM.InteractionManagement.Application.RepositoryInterfaces;
using SalifyCRM.InteractionManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalifyCRM.InteractionManagement.Application.Handlers.InteractionNotes.Commands
{
    public class CreateInteractionNoteCommand : IRequest<IResult>
    {
        public int CreatedUserId { get; set; }

        public int InteractionId { get; set; }
        public string Note { get; set; }
        public DateTime NoteDate { get; set; }
        public string Status { get; set; }

        public class CreateInteractionNoteCommandHandler : IRequestHandler<CreateInteractionNoteCommand, IResult>
        {
            private readonly IInteractionNoteRepository _interactionNoteRepository;
            private readonly IInteractionRepository _interactionRepository;
            private readonly IUserRepository _userRepository;
            private readonly IMediator _mediator;

            public CreateInteractionNoteCommandHandler(
                IInteractionNoteRepository interactionNoteRepository,
                IInteractionRepository interactionRepository,
                IUserRepository userRepository,
                IMediator mediator)
            {
                this._interactionNoteRepository = interactionNoteRepository;
                this._interactionRepository = interactionRepository;
                this._userRepository = userRepository;
                this._mediator = mediator;
            }

            public async Task<IResult> Handle(CreateInteractionNoteCommand request, CancellationToken cancellationToken)
            {
                if (request.InteractionId <= 0)
                {
                    return new ErrorResult()
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

                if (request.CreatedUserId <= 0)
                {
                    return new ErrorResult()
                        .AddErrorDetail(new ErrorDetail
                        {
                            Code = ErrorCode.VAL1002,
                            Message = "The provided value should not be null or negative.",
                            Title = "Null or Negative Value Error"
                        }
                        .AddMetadata("FieldName", "CreatedUserId")
                        .AddMetadata("InputValue", request.CreatedUserId.ToString())
                        );
                }

                var isThereUserRecord = _userRepository.Query().Any(O => O.Id == request.CreatedUserId && O.IsDeleted == false);
                if (!isThereUserRecord)
                {
                    return new ErrorResult()
                        .AddErrorDetail(new ErrorDetail
                        {
                            Code = ErrorCode.BUS4002,
                            Message = "The requested user could not be found.",
                            Title = "User Not Found Error"
                        }
                        .AddMetadata("FieldName", "CreatedUserId")
                        .AddMetadata("InputValue", request.CreatedUserId.ToString())
                        .AddMetadata("Suggestion", "A deleted or non-existing record was requested.")
                        );
                }

                InteractionNote interactionNote = new InteractionNote()
                {
                    LastUpdatedDate = DateTime.UtcNow,
                    LastUpdatedUserId = request.CreatedUserId,
                    CreatedDate = DateTime.UtcNow,
                    CreatedUserId = request.CreatedUserId,
                    InteractionId = request.InteractionId,
                    Note = request.Note,
                    NoteDate = request.NoteDate,
                    Status = request.Status,
                    IsDeleted = false
                };

                _interactionNoteRepository.Update(interactionNote);
                return new SuccessResult();
            }
        }
    }
}
