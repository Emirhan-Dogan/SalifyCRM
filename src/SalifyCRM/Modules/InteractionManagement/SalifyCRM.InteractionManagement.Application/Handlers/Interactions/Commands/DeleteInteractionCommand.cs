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

namespace SalifyCRM.InteractionManagement.Application.Handlers.Interactions.Commands
{
    public class DeleteInteractionCommand : IRequest<IResult>
    {
        public int DeletedUserId { get; set; }
        public int InteractionId { get; set; }

        public class DeleteInteractionCommandHandler : IRequestHandler<DeleteInteractionCommand, IResult>
        {
            private readonly IInteractionRepository _interactionRepository;
            private readonly IUserRepository _userRepository;
            private readonly IMediator _mediator;

            public DeleteInteractionCommandHandler(
                IInteractionRepository interactionRepository,
                IUserRepository userRepository,
                IMediator mediator)
            {
                this._interactionRepository = interactionRepository;
                this._userRepository = userRepository;
                this._mediator = mediator;
            }

            public async Task<IResult> Handle(DeleteInteractionCommand request, CancellationToken cancellationToken)
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
                            Message = "The requested User could not be found.",
                            Title = "User Not Found Error"
                        }
                        .AddMetadata("FieldName", "DeletedUserId")
                        .AddMetadata("InputValue", request.DeletedUserId.ToString())
                        .AddMetadata("Suggestion", "A deleted or non-existing record was requested.")
                        );
                }

                var interaction = await _interactionRepository.GetAsync(O => O.Id == request.InteractionId && O.IsDeleted == false);
                if (interaction == null)
                {
                    return new ErrorResult()
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

                interaction.IsDeleted = true;
                interaction.LastUpdatedDate = DateTime.UtcNow;
                interaction.LastUpdatedUserId = request.DeletedUserId;

                _interactionRepository.Update(interaction);
                return new SuccessResult();
            }
        }
    }
}
