using Core.Utilities.Messages;
using Core.Utilities.Results;
using MediatR;
using SalifyCRM.CustomerManagement.Application.Extensions;
using SalifyCRM.CustomerManagement.Application.RepositoryInterfaces;
using SalifyCRM.CustomerManagement.Application.Responses.CustomerPermissions;
using SalifyCRM.CustomerManagement.Application.Responses.CustomerSocials;
using SalifyCRM.CustomerManagement.Application.Responses.Occupations;
using SalifyCRM.CustomerManagement.Application.Responses.Permissions;
using SalifyCRM.CustomerManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalifyCRM.CustomerManagement.Application.Handlers.CustomerPermissions.Queries
{
    public class GetPermissionsForCustomerQuery : IRequest<IDataResult<List<CustomerPermissionResponse>>>
    {
        public int CustomerId { get; set; }

        public class GetPermissionsForCustomerQueryHandler : IRequestHandler<GetPermissionsForCustomerQuery, IDataResult<List<CustomerPermissionResponse>>>
        {
            private readonly ICustomerPermissionRepository _customerPermissionRepository;
            private readonly ICustomerRepository _customerRepository;
            private readonly IMediator _mediator;

            public GetPermissionsForCustomerQueryHandler(
                ICustomerPermissionRepository customerPermissionRepository,
                ICustomerRepository customerRepository,
                IMediator mediator)
            {
                this._customerPermissionRepository = customerPermissionRepository;
                this._customerRepository = customerRepository;
                this._mediator = mediator;
            }

            public async Task<IDataResult<List<CustomerPermissionResponse>>> Handle(GetPermissionsForCustomerQuery request, CancellationToken cancellationToken)
            {
                if (request.CustomerId <= 0)
                {
                    return new ErrorDataResult<List<CustomerPermissionResponse>>()
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
                    return new ErrorDataResult<List<CustomerPermissionResponse>>()
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

                var customerPermissions = await _customerPermissionRepository.GetListAsync(O => O.CustomerId == request.CustomerId && O.IsDeleted == false);
                List<CustomerPermissionResponse> customerPermissionResponses = CustomerPermissionExtensions.ToCustomerPermissionResponseList(customerPermissions.ToList());

                return new SuccessDataResult<List<CustomerPermissionResponse>>(customerPermissionResponses);
            }
        }
    }
}
