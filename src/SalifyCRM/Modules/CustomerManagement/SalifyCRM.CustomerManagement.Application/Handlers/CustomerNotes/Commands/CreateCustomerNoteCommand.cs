using Core.Utilities.Messages;
using Core.Utilities.Results;
using MediatR;
using SalifyCRM.CustomerManagement.Application.Handlers.CustomerNotes.ValidationRules;
using SalifyCRM.CustomerManagement.Application.Handlers.Occupations.ValidationRules;
using SalifyCRM.CustomerManagement.Application.RepositoryInterfaces;
using SalifyCRM.CustomerManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalifyCRM.CustomerManagement.Application.Handlers.CustomerNotes.Commands
{
    public class CreateCustomerNoteCommand : IRequest<IResult>
    {
        public int CreatedUserId { get; set; }
        public int CustomerId { get; set; }

        public string Note { get; set; }
        public DateTime NoteDate { get; set; }
        public string Status { get; set; }

        public class CreateCustomerNoteCommandHandler : IRequestHandler<CreateCustomerNoteCommand, IResult>
        {
            private readonly ICustomerNoteRepository _customerNoteRepository;
            private readonly ICustomerRepository _customerRepository;
            private readonly IUserRepository _userRepository;
            private readonly IMediator _mediator;

            public CreateCustomerNoteCommandHandler(
                ICustomerNoteRepository customerNoteRepository,
                ICustomerRepository customerRepository,
                IUserRepository userRepository,
                IMediator mediator)
            {
                this._customerNoteRepository = customerNoteRepository;
                this._customerRepository = customerRepository;
                this._userRepository = userRepository;
                this._mediator = mediator;
            }

            public async Task<IResult> Handle(CreateCustomerNoteCommand request, CancellationToken cancellationToken)
            {
                var validator = new CreateCustomerNoteValidator();
                var validationResult = await validator.ValidateAsync(request);

                if (!validationResult.IsValid)
                {
                    var errorResult = new ErrorResult();

                    foreach (var failure in validationResult.Errors)
                    {
                        errorResult.AddErrorDetail(new ErrorDetail
                        {
                            Code = ErrorCode.VAL1004,
                            Message = failure.ErrorMessage,
                            Title = "Validation Error"
                        }
                        .AddMetadata("FieldName", failure.PropertyName)
                        .AddMetadata("InputValue", failure.AttemptedValue?.ToString()));
                    }

                    return errorResult;
                }

                var isThereUserRecord = _userRepository.Query().Any(u => u.Id == request.CreatedUserId);
                if (!isThereUserRecord)
                {
                    return new ErrorResult()
                         .AddErrorDetail(new ErrorDetail
                         {
                             Code = ErrorCode.BUS4003,
                             Message = "No user was found with the provided ID.",
                             Title = "User Not Found"
                         }
                        .AddMetadata("FieldName", "CreatedUserId")
                        .AddMetadata("InputValue", request.CreatedUserId.ToString())
                        );
                }

                var isThereCustomerRecord = _customerRepository.Query().Any(u => u.Id == request.CustomerId);
                if (!isThereCustomerRecord)
                {
                    return new ErrorResult()
                         .AddErrorDetail(new ErrorDetail
                         {
                             Code = ErrorCode.BUS4003,
                             Message = "No customer was found with the provided CustomerId.",
                             Title = "Customer Not Found"
                         }
                        .AddMetadata("FieldName", "CustomerId")
                        .AddMetadata("InputValue", request.CustomerId.ToString())
                        );
                }

                CustomerNote customerNote = new CustomerNote()
                {
                    CreatedUserId = request.CreatedUserId,
                    LastUpdatedUserId = request.CreatedUserId,
                    CreatedDate = DateTime.UtcNow,
                    LastUpdatedDate = DateTime.UtcNow,
                    IsDeleted = false,
                    CustomerId = request.CustomerId,
                    NoteDate = request.NoteDate,
                    Note = request.Note,
                    Status = request.Status
                };

                _customerNoteRepository.Add(customerNote);
                return new SuccessResult("Request Successful.");
            }
        }
    }
}
