using Core.Utilities.Messages;
using Core.Utilities.Results;
using MediatR;
using SalifyCRM.CustomerManagement.Application.Extensions;
using SalifyCRM.CustomerManagement.Application.RepositoryInterfaces;
using SalifyCRM.CustomerManagement.Application.Responses.CustomerPermissions;
using SalifyCRM.CustomerManagement.Application.Responses.CustomerSocials;
using SalifyCRM.CustomerManagement.Application.Responses.CustomerTagMappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalifyCRM.CustomerManagement.Application.Handlers.CustomerSocials.Queries
{
    public class GetSocialMediasForCustomerQuery : IRequest<IDataResult<List<CustomerSocialResponse>>>
    {
        public int CustomerId { get; set; }

        public class GetSocialMediasForCustomerQueryHandler : IRequestHandler<GetSocialMediasForCustomerQuery, IDataResult<List<CustomerSocialResponse>>>
        {
            private readonly ICustomerSocialRepository _customerSocialRepository;
            private readonly ICustomerRepository _customerRepository;
            private readonly IMediator _mediator;

            public GetSocialMediasForCustomerQueryHandler(
                ICustomerSocialRepository customerSocialRepository,
                ICustomerRepository customerRepository,
                IMediator mediator)
            {
                this._customerSocialRepository = customerSocialRepository;
                this._customerRepository = customerRepository;
                this._mediator = mediator;
            }

            public async Task<IDataResult<List<CustomerSocialResponse>>> Handle(GetSocialMediasForCustomerQuery request, CancellationToken cancellationToken)
            {
                if (request.CustomerId <= 0)
                {
                    return new ErrorDataResult<List<CustomerSocialResponse>>()
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
                    return new ErrorDataResult<List<CustomerSocialResponse>>()
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

                var customerSocials = await _customerSocialRepository.GetListAsync(O=>O.CustomerId == request.CustomerId && O.IsDeleted == false);
                List<CustomerSocialResponse> customerSocialResponses = CustomerSocialExtensions.ToCustomerSocialResponseList(customerSocials.ToList());

                return new SuccessDataResult<List<CustomerSocialResponse>>(customerSocialResponses);
            }
        }
    }
}
