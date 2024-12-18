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

namespace SalifyCRM.CustomerManagement.Application.Handlers.Customers.Commands
{
    public class CreateCustomerCommand : IRequest<IResult>
    {
        public int CreatedUserId { get; set; }
        public int Id { get; set; }
        public int CustomerTypeId { get; set; }
        public int OccupationId { get; set; }
        public int MartialStatusId { get; set; }
        public int CustomerCategoryId { get; set; }

        public string CustomerCode { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime BirthDate { get; set; }
        public bool Gender { get; set; }
        public int IncomeLevel { get; set; }
        public string Status { get; set; }

        public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, IResult>
        {
            private readonly ICustomerRepository _customerRepository;
            private readonly ICustomerTypeRepository _customerTypeRepository;
            private readonly IOccupationRepository _occupationRepository;
            private readonly IMartialStatusRepository _mediaStatusRepository;
            private readonly ICustomerCategoryRepository _customerCategoryRepository;
            private readonly IUserRepository _userRepository;
            private readonly IMediator _mediator;

            public CreateCustomerCommandHandler(
                ICustomerRepository customerRepository,
                ICustomerTypeRepository customerTypeRepository,
                IOccupationRepository occupationRepository,
                IMartialStatusRepository mediaStatusRepository,
                ICustomerCategoryRepository customerCategoryRepository,
                IUserRepository userRepository,
                IMediator mediator)
            {
                this._customerRepository = customerRepository;
                this._customerTypeRepository = customerTypeRepository;
                this._occupationRepository = occupationRepository;
                this._mediaStatusRepository = mediaStatusRepository;
                this._customerCategoryRepository = customerCategoryRepository;
                this._userRepository = userRepository;
                this._mediator = mediator;
            }

            public async Task<IResult> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
            {
                if (request.CreatedUserId <= 0)
                {
                    return new ErrorResult()
                        .AddErrorDetail(new ErrorDetail
                        {
                            Code = ErrorCode.VAL1002,
                            Message = "The provided value should not be null or negative.",
                            Title = "Null or Negative Value Error"
                        }
                        .AddMetadata("FieldName", "CreatedUserId")
                        .AddMetadata("InputValue", request.CreatedUserId.ToString())
                        );
                }

                var isThereUserRecord = _userRepository.Query().Any(u => u.Id == request.CreatedUserId && u.IsDeleted == false);
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

                var isThereCustomerRecord = _userRepository.Query().Any(u => u.Email == request.Email && u.IsDeleted == false);
                if (!isThereUserRecord)
                {
                    return new ErrorResult()
                         .AddErrorDetail(new ErrorDetail
                         {
                             Code = ErrorCode.BUS4001,
                             Message = "This email is already exist.",
                             Title = "Email Already Exist"
                         }
                        .AddMetadata("FieldName", "Email")
                        .AddMetadata("InputValue", request.Email.ToString())
                        );
                }

                var isThereCustomerCategoryRecord = _customerCategoryRepository.Query().Any(u => u.Id == request.CustomerCategoryId);
                if (!isThereCustomerCategoryRecord)
                {
                    return new ErrorResult()
                         .AddErrorDetail(new ErrorDetail
                         {
                             Code = ErrorCode.BUS4003,
                             Message = "No customer category was found with the provided CustomerCategoryId.",
                             Title = "Customer Category Not Found"
                         }
                        .AddMetadata("FieldName", "CustomerCategoryId")
                        .AddMetadata("InputValue", request.CustomerCategoryId.ToString())
                        );
                }

                var isThereOccupationRecord = _occupationRepository.Query().Any(u => u.Id == request.OccupationId);
                if (!isThereOccupationRecord)
                {
                    return new ErrorResult()
                         .AddErrorDetail(new ErrorDetail
                         {
                             Code = ErrorCode.BUS4003,
                             Message = "No occupation was found with the provided OccupationId.",
                             Title = "Occupation Not Found"
                         }
                        .AddMetadata("FieldName", "OccupationId")
                        .AddMetadata("InputValue", request.OccupationId.ToString())
                        );
                }

                var isThereCustomerTypeRecord = _customerTypeRepository.Query().Any(u => u.Id == request.CustomerTypeId);
                if (!isThereCustomerTypeRecord)
                {
                    return new ErrorResult()
                         .AddErrorDetail(new ErrorDetail
                         {
                             Code = ErrorCode.BUS4003,
                             Message = "No customer type was found with the provided CustomerTypeId.",
                             Title = "Customer Type Not Found"
                         }
                        .AddMetadata("FieldName", "CustomerTypeId")
                        .AddMetadata("InputValue", request.CustomerTypeId.ToString())
                        );
                }

                var isThereMartialStatusRecord = _mediaStatusRepository.Query().Any(u => u.Id == request.MartialStatusId);
                if (!isThereMartialStatusRecord)
                {
                    return new ErrorResult()
                         .AddErrorDetail(new ErrorDetail
                         {
                             Code = ErrorCode.BUS4003,
                             Message = "No martial status was found with the provided MartialStatusId.",
                             Title = "Martial Status Not Found"
                         }
                        .AddMetadata("FieldName", "MartialStatusId")
                        .AddMetadata("InputValue", request.MartialStatusId.ToString())
                        );
                }

                Customer customer = new Customer()
                {
                    CustomerTypeId = request.CustomerTypeId,
                    OccupationId = request.OccupationId,
                    MartialStatusId = request.MartialStatusId,
                    CustomerCategoryId = request.CustomerCategoryId,
                    LastName = request.LastName,
                    FirstName = request.FirstName,
                    Email = request.Email,
                    PhoneNumber = request.PhoneNumber,
                    BirthDate = request.BirthDate,
                    Gender = request.Gender,
                    IncomeLevel = request.IncomeLevel,
                    Status = request.Status,
                    IsDeleted = false,
                    CreatedDate = DateTime.UtcNow,
                    LastUpdatedDate = DateTime.UtcNow,
                    CreatedUserId = request.CreatedUserId,
                    LastUpdatedUserId = request.CreatedUserId,
                    CustomerCode = request.CustomerCode,
                };

                _customerRepository.Add(customer);
                return new SuccessResult("Request Successful.");
            }
        }
    }
}
