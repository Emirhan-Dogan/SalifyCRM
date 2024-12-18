using Core.Utilities.Messages;
using Core.Utilities.Results;
using MediatR;
using SalifyCRM.CustomerManagement.Application.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalifyCRM.CustomerManagement.Application.Handlers.Addresses.Commands
{
    public class DeleteAddressCommand : IRequest<IResult>
    {
        public int Id { get; set; }
        public int DeletedUserId { get; set; }

        public class DeleteAddressCommandHandler : IRequestHandler<DeleteAddressCommand, IResult>
        {
            private readonly IAddressRepository _addressRepository;
            private readonly IUserRepository _userRepository;
            private readonly IMediator _mediator;

            public DeleteAddressCommandHandler(
                IAddressRepository addressRepository,
                IUserRepository userRepository,
                IMediator mediator)
            {
                this._addressRepository = addressRepository;
                this._userRepository = userRepository;
                this._mediator = mediator;
            }

            public async Task<IResult> Handle(DeleteAddressCommand request, CancellationToken cancellationToken)
            {
                if (request.Id < 0)
                {
                    return new ErrorResult()
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
                if (request.DeletedUserId < 0)
                {
                    return new ErrorResult()
                         .AddErrorDetail(new ErrorDetail
                         {
                             Code = ErrorCode.VAL1002,
                             Message = "The provided value should not be negative.",
                             Title = "Negative Value Error"
                         }
                        .AddMetadata("FieldName", "DeletedUserId")
                        .AddMetadata("InputValue", request.DeletedUserId.ToString())
                        );
                }

                var isThereUserRecord = _userRepository.Query().Any(u => u.Id == request.DeletedUserId);
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

                var address = await _addressRepository.GetAsync(O => O.Id == request.Id && O.IsDeleted == false);
                if(address == null)
                {
                    return new ErrorResult()
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

                address.LastUpdatedDate = DateTime.Now;
                address.LastUpdatedUserId = request.DeletedUserId;
                address.IsDeleted = true;

                _addressRepository.Update(address);
                return new SuccessResult("Request Successful.");
            }
        }
    }
}
