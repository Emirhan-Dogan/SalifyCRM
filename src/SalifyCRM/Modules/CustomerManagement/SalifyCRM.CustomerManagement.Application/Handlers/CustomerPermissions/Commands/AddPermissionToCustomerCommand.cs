using Core.Utilities.Messages;
using Core.Utilities.Results;
using MediatR;
using SalifyCRM.CustomerManagement.Application.RepositoryInterfaces;
using SalifyCRM.CustomerManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalifyCRM.CustomerManagement.Application.Handlers.CustomerPermissions.Commands
{
    public class AddPermissionToCustomerCommand:IRequest<IResult>
    {
        public int CreatedUserId { get; set; }
        public int PermissionId { get; set; }
        public int CustomerId { get; set; }

        public class AddPermissionToCustomerCommandHandler : IRequestHandler<AddPermissionToCustomerCommand, IResult>
        {
            private readonly ICustomerPermissionRepository _customerPermissionRepository;
            private readonly ICustomerRepository _customerRepository;
            private readonly IPermissionRepository _permissionRepository;
            private readonly IUserRepository _userRepository;
            private readonly IMediator _mediator;

            public AddPermissionToCustomerCommandHandler(
                ICustomerPermissionRepository customerPermissionRepository,
                ICustomerRepository customerRepository,
                IPermissionRepository permissionRepository,
                IUserRepository userRepository,
                IMediator mediator)
            {
                this._customerPermissionRepository = customerPermissionRepository;
                this._customerRepository = customerRepository;
                this._permissionRepository = permissionRepository;
                this._userRepository = userRepository;
                this._mediator = mediator;
            }

            public async Task<IResult> Handle(AddPermissionToCustomerCommand request, CancellationToken cancellationToken)
            {
                if (request.CustomerId <= 0)
                {
                    return new ErrorResult()
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

                if (request.PermissionId <= 0)
                {
                    return new ErrorResult()
                        .AddErrorDetail(new ErrorDetail
                        {
                            Code = ErrorCode.VAL1002,
                            Message = "The provided value should not be null or negative.",
                            Title = "Null or Negative Value Error"
                        }
                        .AddMetadata("FieldName", "PermissionId")
                        .AddMetadata("InputValue", request.PermissionId.ToString())
                        );
                }

                var isThereCustomerRecord = _customerRepository.Query().Any(O => O.Id == request.CustomerId && O.IsDeleted == false);
                if (!isThereCustomerRecord)
                {
                    return new ErrorResult()
                         .AddErrorDetail(new ErrorDetail
                         {
                             Code = ErrorCode.BUS4003,
                             Message = "No customer was found with the provided CustomerId.",
                             Title = "Customer Not Found"
                         }
                        .AddMetadata("FieldName", "CustomerId")
                        .AddMetadata("InputValue", request.CustomerId.ToString())
                        );
                }

                var isTherePermissionRecord = _permissionRepository.Query().Any(O => O.Id == request.PermissionId && O.IsDeleted == false);
                if (!isTherePermissionRecord)
                {
                    return new ErrorResult()
                         .AddErrorDetail(new ErrorDetail
                         {
                             Code = ErrorCode.BUS4003,
                             Message = "No permission was found with the provided PermissionId.",
                             Title = "Permission Not Found"
                         }
                        .AddMetadata("FieldName", "PermissionId")
                        .AddMetadata("InputValue", request.PermissionId.ToString())
                        );
                }

                var isThereUserRecord = _userRepository.Query().Any(u => u.Id == request.CreatedUserId);
                if (!isThereUserRecord)
                {
                    return new ErrorResult()
                         .AddErrorDetail(new ErrorDetail
                         {
                             Code = ErrorCode.BUS4003,
                             Message = "No user was found with the provided ID.",
                             Title = "User Not Found"
                         }
                        .AddMetadata("FieldName", "CreatedUserId")
                        .AddMetadata("InputValue", request.CreatedUserId.ToString())
                        );
                }

                var customerPermission = await _customerPermissionRepository.GetAsync(O=>O.CustomerId == request.CustomerId && O.PermissionId == request.PermissionId);
                if (customerPermission != null)
                {
                    customerPermission.IsDeleted = false;
                    customerPermission.LastUpdatedUserId = request.CreatedUserId;
                    customerPermission.LastUpdatedDate = DateTime.UtcNow;

                    _customerPermissionRepository.Update(customerPermission);
                    return new SuccessResult("Request Successful.");
                }

                CustomerPermission newCustomerPermission = new CustomerPermission()
                {
                    PermissionId = request.PermissionId,
                    CustomerId = request.CustomerId,
                    CreatedDate = DateTime.UtcNow,
                    CreatedUserId = request.CreatedUserId,
                    LastUpdatedDate = DateTime.UtcNow,
                    LastUpdatedUserId = request.CreatedUserId,
                    IsDeleted = false
                };

                _customerPermissionRepository.Add(newCustomerPermission);
                return new SuccessResult("Request Successful.");
            }
        }
    }
}
