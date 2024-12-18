using Core.Utilities.Messages;
using Core.Utilities.Results;
using MediatR;
using SalifyCRM.CustomerManagement.Application.Handlers.Districts.ValidationRules;
using SalifyCRM.CustomerManagement.Application.Handlers.Occupations.ValidationRules;
using SalifyCRM.CustomerManagement.Application.RepositoryInterfaces;
using SalifyCRM.CustomerManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalifyCRM.CustomerManagement.Application.Handlers.Districts.Commands
{
    public class CreateDistrictCommand : IRequest<IResult>
    {
        public int CreatedUserId { get; set; }
        public int CityId { get; set; }

        public string Name { get; set; }
        public string Status { get; set; }


        public class CreateDistrictCommandHandler : IRequestHandler<CreateDistrictCommand, IResult>
        {
            private readonly IDistrictRepository _districtRepository;
            private readonly ICityRepository _cityRepository;
            private readonly IUserRepository _userRepository;
            private readonly IMediator _mediator;

            public CreateDistrictCommandHandler(
                IDistrictRepository districtRepository,
                ICityRepository cityRepository,
                IUserRepository userRepository,
                IMediator mediator)
            {
                this._districtRepository = districtRepository;
                this._cityRepository = cityRepository;
                this._userRepository = userRepository;
                this._mediator = mediator;
            }

            public async Task<IResult> Handle(CreateDistrictCommand request, CancellationToken cancellationToken)
            {
                var validator = new CreateDistrictValidator();
                var validationResult = await validator.ValidateAsync(request);

                if (!validationResult.IsValid)
                {
                    var errorResult = new ErrorResult();

                    foreach (var failure in validationResult.Errors)
                    {
                        errorResult.AddErrorDetail(new ErrorDetail
                        {
                            Code = ErrorCode.VAL1004,
                            Message = failure.ErrorMessage,
                            Title = "Validation Error"
                        }
                        .AddMetadata("FieldName", failure.PropertyName)
                        .AddMetadata("InputValue", failure.AttemptedValue?.ToString()));
                    }

                    return errorResult;
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

                var isThereCityRecord = _cityRepository.Query().Any(u => u.Id == request.CityId);
                if (!isThereCityRecord)
                {
                    return new ErrorResult()
                         .AddErrorDetail(new ErrorDetail
                         {
                             Code = ErrorCode.BUS4003,
                             Message = "No city was found with the provided CityId.",
                             Title = "City Not Found"
                         }
                        .AddMetadata("FieldName", "CityId")
                        .AddMetadata("InputValue", request.CityId.ToString())
                        );
                }

                District district = new District()
                {
                    CreatedDate = DateTime.UtcNow,
                    LastUpdatedDate = DateTime.UtcNow,
                    CreatedUserId = request.CreatedUserId,
                    LastUpdatedUserId = request.CreatedUserId,
                    CityId = request.CityId,
                    Name = request.Name,
                    Status = request.Status,
                    IsDeleted = false
                };

                _districtRepository.Add(district);
                return new SuccessResult("Request Successful.");
            }
        }
    }
}
