using Core.Utilities.Messages;
using Core.Utilities.Results;
using MediatR;
using SalifyCRM.CustomerManagement.Application.Extensions;
using SalifyCRM.CustomerManagement.Application.RepositoryInterfaces;
using SalifyCRM.CustomerManagement.Application.Responses.Addresses;
using SalifyCRM.CustomerManagement.Application.Responses.Cities;
using SalifyCRM.CustomerManagement.Application.Responses.Countries;
using SalifyCRM.CustomerManagement.Application.Responses.CustomerTagMappings;
using SalifyCRM.CustomerManagement.Application.Responses.Districts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalifyCRM.CustomerManagement.Application.Handlers.Districts.Queries
{
    public class GetDistrictQuery : IRequest<IDataResult<DistrictDetailResponse>>
    {
        public int Id { get; set; }

        public class GetDistrictQueryHandler : IRequestHandler<GetDistrictQuery, IDataResult<DistrictDetailResponse>>
        {
            private readonly IDistrictRepository _districtRepository;
            private readonly IMediator _mediator;

            public GetDistrictQueryHandler(
                IDistrictRepository districtRepository,
                IMediator mediator)
            {
                this._districtRepository = districtRepository;
                this._mediator = mediator;
            }

            public async Task<IDataResult<DistrictDetailResponse>> Handle(GetDistrictQuery request, CancellationToken cancellationToken)
            {
                if (request.Id <= 0)
                {
                    return new ErrorDataResult<DistrictDetailResponse>()
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

                var district = await _districtRepository.GetAsync(O => O.Id == request.Id && O.IsDeleted == false);
                if(district == null)
                {
                    return new ErrorDataResult<DistrictDetailResponse>()
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
                DistrictDetailResponse districtDetailResponse = DistrictExtensions.ToDistrictDetailResponse(district);

                return new SuccessDataResult<DistrictDetailResponse>(districtDetailResponse);
            }
        }
    }
}
