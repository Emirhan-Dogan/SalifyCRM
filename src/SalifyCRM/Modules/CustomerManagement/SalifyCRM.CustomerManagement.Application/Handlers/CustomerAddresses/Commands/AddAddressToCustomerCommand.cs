using Core.Utilities.Messages;
using Core.Utilities.Results;
using MediatR;
using SalifyCRM.CustomerManagement.Application.RepositoryInterfaces;
using SalifyCRM.CustomerManagement.Application.Responses.Cities;
using SalifyCRM.CustomerManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalifyCRM.CustomerManagement.Application.Handlers.CustomerAddresses.Commands
{
    public class AddAddressToCustomerCommand : IRequest<IResult>
    {
        public int CreatedUserId { get; set; }
        public int AddressId { get; set; }
        public int CustomerId { get; set; }

        public class AddAddressToCustomerCommandHandler : IRequestHandler<AddAddressToCustomerCommand, IResult>
        {
            private readonly ICustomerAddressRepository _customerAddressRepository;
            private readonly ICustomerRepository _customerRepository;
            private readonly IAddressRepository _addressRepository;
            private readonly IUserRepository _userRepository;
            private readonly IMediator _mediator;

            public AddAddressToCustomerCommandHandler(
                ICustomerAddressRepository customerAddressRepository,
                ICustomerRepository customerRepository,
                IAddressRepository addressRepository,
                IUserRepository userRepository,
                IMediator mediator)
            {
                this._customerAddressRepository = customerAddressRepository;
                this._customerRepository = customerRepository;
                this._addressRepository = addressRepository;
                this._userRepository = userRepository;
                this._mediator = mediator;
            }

            public async Task<IResult> Handle(AddAddressToCustomerCommand request, CancellationToken cancellationToken)
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

                if (request.AddressId <= 0)
                {
                    return new ErrorResult()
                        .AddErrorDetail(new ErrorDetail
                        {
                            Code = ErrorCode.VAL1002,
                            Message = "The provided value should not be null or negative.",
                            Title = "Null or Negative Value Error"
                        }
                        .AddMetadata("FieldName", "AddressId")
                        .AddMetadata("InputValue", request.AddressId.ToString())
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

                var isThereAddressRecord = _addressRepository.Query().Any(O => O.Id == request.AddressId && O.IsDeleted == false);
                if (!isThereAddressRecord)
                {
                    return new ErrorResult()
                         .AddErrorDetail(new ErrorDetail
                         {
                             Code = ErrorCode.BUS4003,
                             Message = "No address was found with the provided AddressId.",
                             Title = "Address Not Found"
                         }
                        .AddMetadata("FieldName", "AddressId")
                        .AddMetadata("InputValue", request.AddressId.ToString())
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

                var customerAddress = await _customerAddressRepository.GetAsync(O => O.CustomerId == request.CustomerId && O.AddressId == request.AddressId);
                if(customerAddress != null)
                {
                    customerAddress.IsDeleted = false;
                    customerAddress.LastUpdatedUserId = request.CreatedUserId;
                    customerAddress.LastUpdatedDate = DateTime.UtcNow;

                    _customerAddressRepository.Update(customerAddress);
                    return new SuccessResult("Request Successful.");
                }

                CustomerAddress newCustomerAddress = new CustomerAddress()
                {
                    AddressId = request.AddressId,
                    CustomerId = request.CustomerId,
                    CreatedDate = DateTime.UtcNow,
                    CreatedUserId = request.CreatedUserId,
                    LastUpdatedDate = DateTime.UtcNow,
                    LastUpdatedUserId = request.CreatedUserId,
                    IsDeleted = false
                };

                _customerAddressRepository.Add(newCustomerAddress);
                return new SuccessResult("Request Successful.");
            }
        }
    }
}
