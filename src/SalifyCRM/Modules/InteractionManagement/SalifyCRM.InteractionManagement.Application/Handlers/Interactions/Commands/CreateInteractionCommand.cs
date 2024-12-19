using Core.Utilities.Messages;
using Core.Utilities.Results;
using MediatR;
using SalifyCRM.InteractionManagement.Application.RepositoryInterfaces;
using SalifyCRM.InteractionManagement.Application.Responses.InteractionNotes;
using SalifyCRM.InteractionManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalifyCRM.InteractionManagement.Application.Handlers.Interactions.Commands
{
    public class CreateInteractionCommand : IRequest<IResult>
    {
        public int CreatedUserId { get; set; }

        public int InteractionTypeId { get; set; }
        public int CustomerId { get; set; }
        public int? OrderId { get; set; }

        public string? OrderCode { get; set; }
        public DateTime InteractionDate { get; set; }
        public string Descriptions { get; set; }
        public string Status { get; set; }

        public List<InteractionNoteResponse> InteractionNotes { get; set; }

        public class CreateInteractionCommandHandler : IRequestHandler<CreateInteractionCommand, IResult>
        {
            private readonly IInteractionRepository _interactionRepository;
            private readonly IInteractionTypeRepository _interactionTypeRepository;
            private readonly IOrderRepository _orderRepository;
            private readonly ICustomerRepository _customerRepository;
            private readonly IUserRepository _userRepository;
            private readonly IMediator _mediator;

            public CreateInteractionCommandHandler(
                IInteractionRepository interactionRepository,
                IInteractionTypeRepository interactionTypeRepository,
                IOrderRepository orderRepository,
                ICustomerRepository customerRepository,
                IUserRepository userRepository,
                IMediator mediator)
            {
                this._interactionRepository = interactionRepository;
                this._interactionTypeRepository = interactionTypeRepository;
                this._orderRepository = orderRepository;
                this._customerRepository = customerRepository;
                this._userRepository = userRepository;
                this._mediator = mediator;
            }

            public async Task<IResult> Handle(CreateInteractionCommand request, CancellationToken cancellationToken)
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

                if (request.CustomerId <= 0)
                {
                    return new ErrorResult()
                        .AddErrorDetail(new ErrorDetail
                        {
                            Code = ErrorCode.VAL1002,
                            Message = "The provided value should not be null or negative.",
                            Title = "Null or Negative Value Error"
                        }
                        .AddMetadata("FieldName", "CustomerId")
                        .AddMetadata("InputValue", request.CustomerId.ToString())
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

                var isThereCustomerRecord = _customerRepository.Query().Any(O => O.Id == request.CustomerId && O.IsDeleted == false);
                if (!isThereCustomerRecord)
                {
                    return new ErrorResult()
                        .AddErrorDetail(new ErrorDetail
                        {
                            Code = ErrorCode.BUS4002,
                            Message = "The requested Data could not be found.",
                            Title = "Data Not Found Error"
                        }
                        .AddMetadata("FieldName", "CustomerId")
                        .AddMetadata("InputValue", request.CustomerId.ToString())
                        .AddMetadata("Suggestion", "A deleted or non-existing record was requested.")
                        );
                }


                Interaction interaction = new Interaction()
                {
                    CreatedDate = DateTime.UtcNow,
                    LastUpdatedDate = DateTime.UtcNow,
                    LastUpdatedUserId = request.CreatedUserId,
                    CreatedUserId = request.CreatedUserId,
                    Status = request.Status,
                    OrderId = request.OrderId,
                    OrderCode = request.OrderCode,
                    InteractionTypeId = request.InteractionTypeId,
                    Descriptions = request.Descriptions,
                    CustomerId = request.CustomerId,
                    IsDeleted = false,
                    InteractionDate = request.InteractionDate
                };

                _interactionRepository.Add(interaction);
                return new SuccessResult();
            }
        }
    }
}
