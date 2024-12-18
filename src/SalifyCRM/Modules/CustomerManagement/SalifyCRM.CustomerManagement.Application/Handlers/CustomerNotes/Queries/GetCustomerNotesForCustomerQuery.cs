using Core.Utilities.Messages;
using Core.Utilities.Results;
using MediatR;
using SalifyCRM.CustomerManagement.Application.Extensions;
using SalifyCRM.CustomerManagement.Application.RepositoryInterfaces;
using SalifyCRM.CustomerManagement.Application.Responses.Addresses;
using SalifyCRM.CustomerManagement.Application.Responses.CustomerNotes;
using SalifyCRM.CustomerManagement.Application.Responses.CustomerTagMappings;
using SalifyCRM.CustomerManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalifyCRM.CustomerManagement.Application.Handlers.CustomerNotes.Queries
{
    public class GetCustomerNotesForCustomerQuery : IRequest<IDataResult<List<CustomerNoteResponse>>>
    {
        public int CustomerId { get; set; }

        public class GetCustomerNotesForCustomerQueryHandler : IRequestHandler<GetCustomerNotesForCustomerQuery, IDataResult<List<CustomerNoteResponse>>>
        {
            private readonly ICustomerNoteRepository _customerNoteRepository;
            private readonly ICustomerRepository _customerRepository;
            private readonly IMediator _mediator;

            public GetCustomerNotesForCustomerQueryHandler(
                ICustomerNoteRepository customerNoteRepository,
                ICustomerRepository customerRepository,
                IMediator mediator)
            {
                this._customerNoteRepository = customerNoteRepository;
                this._customerRepository = customerRepository;
                this._mediator = mediator;
            }

            public async Task<IDataResult<List<CustomerNoteResponse>>> Handle(GetCustomerNotesForCustomerQuery request, CancellationToken cancellationToken)
            {
                if (request.CustomerId <= 0)
                {
                    return new ErrorDataResult<List<CustomerNoteResponse>>()
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

                var isThereCustomerRecord = _customerRepository.Query().Any(O => O.Id == request.CustomerId && O.IsDeleted == false);
                if (!isThereCustomerRecord)
                {
                    return new ErrorDataResult<List<CustomerNoteResponse>>()
                         .AddErrorDetail(new ErrorDetail
                         {
                             Code = ErrorCode.BUS4003,
                             Message = "No customer was found with the provided ID.",
                             Title = "Customer Not Found"
                         }
                        .AddMetadata("FieldName", "CustomerId")
                        .AddMetadata("InputValue", request.CustomerId.ToString())
                        );
                }

                var customerNotes = await _customerNoteRepository.GetListAsync(O => O.CustomerId == request.CustomerId && O.IsDeleted == false);
                List<CustomerNoteResponse> customerNoteResponses = CustomerNoteExtensions.ToCustomerNoteResponseList(customerNotes.ToList());

                return new SuccessDataResult<List<CustomerNoteResponse>>(customerNoteResponses);
            }
        }
    }
}
