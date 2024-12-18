using Core.Utilities.Messages;
using Core.Utilities.Results;
using MediatR;
using SalifyCRM.CustomerManagement.Application.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalifyCRM.CustomerManagement.Application.Handlers.Customers.Commands
{
    public class DeleteCustomerCommand : IRequest<IResult>
    {
        public int Id { get; set; }
        public int DeletedUserId { get; set; }

        public class DeleteCustomerCommandHandler : IRequestHandler<DeleteCustomerCommand, IResult>
        {
            private readonly ICustomerRepository _customerRepository;
            private readonly IUserRepository _userRepository;
            private readonly IMediator _mediator;

            public DeleteCustomerCommandHandler(
                ICustomerRepository customerRepository,
                IUserRepository userRepository,
                IMediator mediator)
            {
                this._customerRepository = customerRepository;
                this._userRepository = userRepository;
                this._mediator = mediator;
            }

            public async Task<IResult> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
            {
                if (request.Id <= 0)
                {
                    return new ErrorResult()
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

                if (request.DeletedUserId <= 0)
                {
                    return new ErrorResult()
                        .AddErrorDetail(new ErrorDetail
                        {
                            Code = ErrorCode.VAL1002,
                            Message = "The provided value should not be null or negative.",
                            Title = "Null or Negative Value Error"
                        }
                        .AddMetadata("FieldName", "DeletedUserId")
                        .AddMetadata("InputValue", request.DeletedUserId.ToString())
                        );
                }

                var isThereUserRecord = _userRepository.Query().Any(u => u.Id == request.DeletedUserId && u.IsDeleted == false);
                if (!isThereUserRecord)
                {
                    return new ErrorResult()
                         .AddErrorDetail(new ErrorDetail
                         {
                             Code = ErrorCode.BUS4003,
                             Message = "No user was found with the provided ID.",
                             Title = "User Not Found"
                         }
                        .AddMetadata("FieldName", "DeletedUserId")
                        .AddMetadata("InputValue", request.DeletedUserId.ToString())
                        );
                }

                var customer = await _customerRepository.GetAsync(O => O.Id == request.Id && O.IsDeleted == false);
                if(customer == null)
                {
                    return new ErrorResult()
                         .AddErrorDetail(new ErrorDetail
                         {
                             Code = ErrorCode.BUS4003,
                             Message = "No customer was found with the provided ID.",
                             Title = "Customer Not Found"
                         }
                        .AddMetadata("FieldName", "Id")
                        .AddMetadata("InputValue", request.Id.ToString())
                        );
                }

                customer.IsDeleted = true;
                customer.LastUpdatedDate = DateTime.UtcNow;
                customer.LastUpdatedUserId = request.DeletedUserId;
                
                _customerRepository.Update( customer );
                return new SuccessResult("Request Successful.");
            }
        }
    }
}
