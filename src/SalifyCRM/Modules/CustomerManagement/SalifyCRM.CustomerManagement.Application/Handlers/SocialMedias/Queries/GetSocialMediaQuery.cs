using Core.Utilities.Messages;
using Core.Utilities.Results;
using MediatR;
using SalifyCRM.CustomerManagement.Application.Extensions;
using SalifyCRM.CustomerManagement.Application.RepositoryInterfaces;
using SalifyCRM.CustomerManagement.Application.Responses.Customers;
using SalifyCRM.CustomerManagement.Application.Responses.SocialMedias;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalifyCRM.CustomerManagement.Application.Handlers.SocialMedias.Queries
{
    public class GetSocialMediaQuery : IRequest<IDataResult<SocialMediaDetailResponse>>
    {
        public int Id { get; set; }

        public class GetSocialMediaQueryHandler : IRequestHandler<GetSocialMediaQuery, IDataResult<SocialMediaDetailResponse>>
        {
            private readonly ISocialMediaRepository _socialMediaRepository;
            private readonly ICustomerRepository _customerRepository;
            private readonly IMediator _mediator;

            public GetSocialMediaQueryHandler(ISocialMediaRepository socialMediaRepository, ICustomerRepository customerRepository, IMediator mediator)
            {
                this._socialMediaRepository = socialMediaRepository;
                this._customerRepository = customerRepository;
                this._mediator = mediator;
            }

            public async Task<IDataResult<SocialMediaDetailResponse>> Handle(GetSocialMediaQuery request, CancellationToken cancellationToken)
            {
                if (request.Id == null)
                {
                    return new ErrorDataResult<SocialMediaDetailResponse>()
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
                    return new ErrorDataResult<SocialMediaDetailResponse>()
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

                var socialMedia = await _socialMediaRepository.GetAsync(O => O.Id == request.Id && O.IsDeleted == false);
                if( socialMedia == null)
                {
                    return new ErrorDataResult<SocialMediaDetailResponse>()
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

                var customers = _customerRepository.GetCustomersBySocialMediaId(request.Id);
                List<CustomerResponse> customerResponses = CustomerExtensions.ToCustomerResponseList(customers.ToList());


                SocialMediaDetailResponse result = SocialMediaExtensions.ToSocialMediaDetailResponse(socialMedia, customerResponses);

                return new SuccessDataResult<SocialMediaDetailResponse>(result);
            }
        }
    }
}
