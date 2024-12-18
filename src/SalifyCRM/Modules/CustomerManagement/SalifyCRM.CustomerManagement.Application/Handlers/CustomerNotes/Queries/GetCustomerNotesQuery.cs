using Core.Utilities.Results;
using MediatR;
using SalifyCRM.CustomerManagement.Application.Extensions;
using SalifyCRM.CustomerManagement.Application.RepositoryInterfaces;
using SalifyCRM.CustomerManagement.Application.Responses.CustomerNotes;
using SalifyCRM.CustomerManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalifyCRM.CustomerManagement.Application.Handlers.CustomerNotes.Queries
{
    public class GetCustomerNotesQuery : IRequest<IDataResult<List<CustomerNoteResponse>>>
    {

        public class GetCustomerNotesQueryHandler : IRequestHandler<GetCustomerNotesQuery, IDataResult<List<CustomerNoteResponse>>>
        {
            private readonly ICustomerNoteRepository _customerNoteRepository;
            // private readonly ICustomerRepository _customerRepository;
            private readonly IMediator _mediator;

            public GetCustomerNotesQueryHandler(
                ICustomerNoteRepository customerNoteRepository,
                IMediator mediator)
            {
                this._customerNoteRepository = customerNoteRepository;
                this._mediator = mediator;
            }

            public async Task<IDataResult<List<CustomerNoteResponse>>> Handle(GetCustomerNotesQuery request, CancellationToken cancellationToken)
            {
                var customerNotes = await _customerNoteRepository.GetListAsync(O => O.IsDeleted == false);
                List<CustomerNoteResponse> customerNoteResponses = CustomerNoteExtensions.ToCustomerNoteResponseList(customerNotes.ToList());

                return new SuccessDataResult<List<CustomerNoteResponse>>(customerNoteResponses);
            }
        }
    }
}
