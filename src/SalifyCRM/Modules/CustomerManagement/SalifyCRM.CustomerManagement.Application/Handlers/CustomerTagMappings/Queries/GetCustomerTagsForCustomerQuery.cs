using Core.Utilities.Messages;
using Core.Utilities.Results;
using MediatR;
using SalifyCRM.CustomerManagement.Application.Extensions;
using SalifyCRM.CustomerManagement.Application.RepositoryInterfaces;
using SalifyCRM.CustomerManagement.Application.Responses.CustomerSocials;
using SalifyCRM.CustomerManagement.Application.Responses.CustomerTagMappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalifyCRM.CustomerManagement.Application.Handlers.CustomerTagMappings.Queries
{
    public class GetCustomerTagsForCustomerQuery : IRequest<IDataResult<List<CustomerTagMappingResponse>>>
    {
        public int CustomerId { get; set; }

        public class GetCustomerTagsForCustomerQueryHandler : IRequestHandler<GetCustomerTagsForCustomerQuery, IDataResult<List<CustomerTagMappingResponse>>>
        {
            private readonly ICustomerTagMappingRepository _customerTagMappingRepository;
            private readonly ICustomerRepository _customerRepository;
            private readonly IMediator _mediator;

            public GetCustomerTagsForCustomerQueryHandler(
                ICustomerTagMappingRepository customerTagMappingRepository,
                ICustomerRepository customerRepository,
                IMediator mediator)
            {
                this._customerTagMappingRepository = customerTagMappingRepository;
                this._customerRepository = customerRepository;
                this._mediator = mediator;
            }

            public async Task<IDataResult<List<CustomerTagMappingResponse>>> Handle(GetCustomerTagsForCustomerQuery request, CancellationToken cancellationToken)
            {
                if (request.CustomerId <= 0)
                {
                    return new ErrorDataResult<List<CustomerTagMappingResponse>>()
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
                    return new ErrorDataResult<List<CustomerTagMappingResponse>>()
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

                var customerTagMappings = await _customerTagMappingRepository.GetListAsync(O => O.CustomerId == request.CustomerId && O.IsDeleted == false);
                List<CustomerTagMappingResponse> customerTagMappingResponses = CustomerTagMappingExtensions.ToCustomerTagMappingResponseList(customerTagMappings.ToList());

                return new SuccessDataResult<List<CustomerTagMappingResponse>>(customerTagMappingResponses);
            }
        }
    }
}
