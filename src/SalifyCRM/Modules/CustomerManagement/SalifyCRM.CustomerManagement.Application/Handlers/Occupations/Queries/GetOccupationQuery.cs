using Core.Utilities.Messages;
using Core.Utilities.Results;
using MediatR;
using SalifyCRM.CustomerManagement.Application.Extensions;
using SalifyCRM.CustomerManagement.Application.RepositoryInterfaces;
using SalifyCRM.CustomerManagement.Application.Responses.Customers;
using SalifyCRM.CustomerManagement.Application.Responses.Occupations;
using SalifyCRM.CustomerManagement.Application.Responses.Permissions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalifyCRM.CustomerManagement.Application.Handlers.Occupations.Queries
{
    public class GetOccupationQuery : IRequest<IDataResult<OccupationDetailResponse>>
    {
        public int Id { get; set; }

        public class GetOccupationQueryHandler : IRequestHandler<GetOccupationQuery, IDataResult<OccupationDetailResponse>>
        {
            private readonly IOccupationRepository _occupationRepository;
            private readonly ICustomerRepository _customerRepository;
            private readonly IMediator _mediator;

            public GetOccupationQueryHandler(IOccupationRepository occupationRepository, ICustomerRepository customerRepository, IMediator mediator)
            {
                this._occupationRepository = occupationRepository;
                this._customerRepository = customerRepository;
                this._mediator = mediator;
            }

            public async Task<IDataResult<OccupationDetailResponse>> Handle(GetOccupationQuery request, CancellationToken cancellationToken)
            {
                if (request.Id == null)
                {
                    return new ErrorDataResult<OccupationDetailResponse>()
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
                    return new ErrorDataResult<OccupationDetailResponse>()
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

                var occupation = await _occupationRepository.GetAsync(O => O.Id == request.Id && O.IsDeleted == false);
                if (occupation == null)
                {
                    return new ErrorDataResult<OccupationDetailResponse>()
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

                var customers = await _customerRepository.GetListAsync(O => O.OccupationId == request.Id && O.IsDeleted == false);
                List<CustomerResponse> customerResponses = CustomerExtensions.ToCustomerResponseList(customers.ToList());


                OccupationDetailResponse result = OccupationExtensions.ToOccupationDetailResponse(occupation, customerResponses);

                return new SuccessDataResult<OccupationDetailResponse>(result);
            }
        }
    }
}
