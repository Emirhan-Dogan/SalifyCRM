using Core.Utilities.Messages;
using Core.Utilities.Results;
using MediatR;
using SalifyCRM.CustomerManagement.Application.Extensions;
using SalifyCRM.CustomerManagement.Application.RepositoryInterfaces;
using SalifyCRM.CustomerManagement.Application.Responses.Customers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalifyCRM.CustomerManagement.Application.Handlers.Customers.Queries
{
    public class GetCustomersForOccupationQuery : IRequest<IDataResult<List<CustomerResponse>>>
    {
        public int OccupationId { get; set; }

        public class GetCustomersForOccupationQueryHandler : IRequestHandler<GetCustomersForOccupationQuery, IDataResult<List<CustomerResponse>>>
        {
            private readonly ICustomerRepository _customerRepository;
            private readonly IOccupationRepository _occupationRepository;
            private readonly IMediator _mediator;

            public GetCustomersForOccupationQueryHandler(
                ICustomerRepository customerRepository,
                IOccupationRepository occupationRepository,
                IMediator mediator)
            {
                this._customerRepository = customerRepository;
                this._occupationRepository = occupationRepository;
                this._mediator = mediator;
            }

            public async Task<IDataResult<List<CustomerResponse>>> Handle(GetCustomersForOccupationQuery request, CancellationToken cancellationToken)
            {
                if (request.OccupationId <= 0)
                {
                    return new ErrorDataResult<List<CustomerResponse>>()
                        .AddErrorDetail(new ErrorDetail
                        {
                            Code = ErrorCode.VAL1002,
                            Message = "The provided value should not be null or negative.",
                            Title = "Null or Negative Value Error"
                        }
                        .AddMetadata("FieldName", "OccupationId")
                        .AddMetadata("InputValue", request.OccupationId.ToString())
                        );
                }

                var isThereOccupationRecord = _occupationRepository.Query().Any(u => u.Id == request.OccupationId);
                if (!isThereOccupationRecord)
                {
                    return new ErrorDataResult<List<CustomerResponse>>()
                         .AddErrorDetail(new ErrorDetail
                         {
                             Code = ErrorCode.BUS4003,
                             Message = "No occupation was found with the provided ID.",
                             Title = "Occupation Not Found"
                         }
                        .AddMetadata("FieldName", "OccupationId")
                        .AddMetadata("InputValue", request.OccupationId.ToString())
                        );
                }

                var customers = await _customerRepository.GetListAsync(O => O.OccupationId == request.OccupationId && O.IsDeleted == false);
                List<CustomerResponse> customerResponses = CustomerExtensions.ToCustomerResponseList(customers.ToList());

                return new SuccessDataResult<List<CustomerResponse>>(customerResponses);
            }
        }
    }
}
