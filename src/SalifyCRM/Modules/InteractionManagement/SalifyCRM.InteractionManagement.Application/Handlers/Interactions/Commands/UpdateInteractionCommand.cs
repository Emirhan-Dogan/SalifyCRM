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

namespace SalifyCRM.InteractionManagement.Application.Handlers.Interactions.Commands
{
    public class UpdateInteractionCommand : IRequest<IResult>
    {
        public int LastUpdatedUserId { get; set; }

        public int Id { get; set; }
        public int InteractionTypeId { get; set; }
        public int? OrderId { get; set; }

        public string? OrderCode { get; set; }
        public string Descriptions { get; set; }
        public string Status { get; set; }

        public class UpdateInteractionCommandHandler : IRequestHandler<UpdateInteractionCommand, IResult>
        {
            private readonly IInteractionRepository _interactionRepository;
            private readonly IInteractionTypeRepository _interactionTypeRepository;
            private readonly IOrderRepository _orderRepository;
            private readonly IUserRepository _userRepository;
            private readonly IMediator _mediator;

            public UpdateInteractionCommandHandler(
                IInteractionRepository interactionRepository,
                IInteractionTypeRepository interactionTypeRepository,
                IOrderRepository orderRepository,
                IUserRepository userRepository,
                IMediator mediator)
            {
                this._interactionRepository = interactionRepository;
                this._interactionTypeRepository = interactionTypeRepository;
                this._orderRepository = orderRepository;
                this._userRepository = userRepository;
                this._mediator = mediator;
            }

            public async Task<IResult> Handle(UpdateInteractionCommand request, CancellationToken cancellationToken)
            {
                if (request.InteractionTypeId <= 0)
                {
                    return new ErrorResult()
                        .AddErrorDetail(new ErrorDetail
                        {
                            Code = ErrorCode.VAL1002,
                            Message = "The provided value should not be null or negative.",
                            Title = "Null or Negative Value Error"
                        }
                        .AddMetadata("FieldName", "InteractionTypeId")
                        .AddMetadata("InputValue", request.InteractionTypeId.ToString())
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

                var isThereInteractionTypeRecord = _interactionTypeRepository.Query().Any(O => O.Id == request.InteractionTypeId && O.IsDeleted == false);
                if (!isThereInteractionTypeRecord)
                {
                    return new ErrorResult()
                        .AddErrorDetail(new ErrorDetail
                        {
                            Code = ErrorCode.BUS4002,
                            Message = "The requested data could not be found.",
                            Title = "Data Not Found Error"
                        }
                        .AddMetadata("FieldName", "InteractionTypeId")
                        .AddMetadata("InputValue", request.InteractionTypeId.ToString())
                        .AddMetadata("Suggestion", "A deleted or non-existing record was requested.")
                        );
                }

                var interaction = await _interactionRepository.GetAsync(O => O.Id == request.Id && O.IsDeleted == false);
                if (interaction == null)
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

                interaction.InteractionTypeId = request.InteractionTypeId;
                interaction.OrderId = request.OrderId;
                interaction.OrderCode = request.OrderCode;
                interaction.Descriptions = request.Descriptions;
                interaction.Status = request.Status;

                interaction.LastUpdatedDate = DateTime.UtcNow;
                interaction.LastUpdatedUserId = request.LastUpdatedUserId;

                _interactionRepository.Update(interaction);
                return new SuccessResult();
            }
        }
    }
}
