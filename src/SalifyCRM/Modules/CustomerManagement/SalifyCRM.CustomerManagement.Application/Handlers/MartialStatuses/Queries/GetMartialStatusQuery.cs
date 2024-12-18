using Core.Utilities.Messages;
using Core.Utilities.Results;
using MediatR;
using SalifyCRM.CustomerManagement.Application.Extensions;
using SalifyCRM.CustomerManagement.Application.RepositoryInterfaces;
using SalifyCRM.CustomerManagement.Application.Responses.Customers;
using SalifyCRM.CustomerManagement.Application.Responses.MartialStatuses;
using SalifyCRM.CustomerManagement.Application.Responses.Permissions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalifyCRM.CustomerManagement.Application.Handlers.MartialStatuses.Queries
{
    public class GetMartialStatusQuery : IRequest<IDataResult<MartialStatusDetailResponse>>
    {
        public int Id { get; set; }

        public class GetMartialStatusQueryHandler : IRequestHandler<GetMartialStatusQuery, IDataResult<MartialStatusDetailResponse>>
        {
            private readonly IMartialStatusRepository _martialStatusRepository;
            private readonly ICustomerRepository _customerRepository;
            private readonly IMediator _mediator;

            public GetMartialStatusQueryHandler(IMartialStatusRepository martialStatusRepository, ICustomerRepository customerRepository, IMediator mediator)
            {
                this._martialStatusRepository = martialStatusRepository;
                this._customerRepository = customerRepository;
                this._mediator = mediator;
            }

            public async Task<IDataResult<MartialStatusDetailResponse>> Handle(GetMartialStatusQuery request, CancellationToken cancellationToken)
            {
                if (request.Id == null)
                {
                    return new ErrorDataResult<MartialStatusDetailResponse>()
                        .AddErrorDetail(new ErrorDetail
                        {
                            Code = ErrorCode.VAL1001,
                            Message = "Required field is missing.",
                            Title = "Missing Field Error"
                        }
                        .AddMetadata("FieldName", "Id")
                        .AddMetadata("InputValue", "Null")
                        );
                }
                if (request.Id < 0)
                {
                    return new ErrorDataResult<MartialStatusDetailResponse>()
                        .AddErrorDetail(new ErrorDetail
                        {
                            Code = ErrorCode.VAL1002,
                            Message = "The provided value should not be negative.",
                            Title = "Negative Value Error"
                        }
                        .AddMetadata("FieldName", "Id")
                        .AddMetadata("InputValue", request.Id.ToString())
                        );
                }

                var martialStatus = await _martialStatusRepository.GetAsync(O => O.Id == request.Id && O.IsDeleted == false);
                if (martialStatus == null)
                {
                    return new ErrorDataResult<MartialStatusDetailResponse>()
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

                var customers = await _customerRepository.GetListAsync(O => O.MartialStatusId == request.Id && O.IsDeleted == false);
                List<CustomerResponse> customerResponses = CustomerExtensions.ToCustomerResponseList(customers.ToList());

                MartialStatusDetailResponse result = MartialStatusExtensions.ToMartialStatusDetailResponse(martialStatus, customerResponses);

                return new SuccessDataResult<MartialStatusDetailResponse>(result);
            }
        }
    }
}
