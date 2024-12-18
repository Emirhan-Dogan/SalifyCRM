using Core.Utilities.Messages;
using Core.Utilities.Results;
using MediatR;
using SalifyCRM.CustomerManagement.Application.Handlers.Cities.ValidationRules;
using SalifyCRM.CustomerManagement.Application.Handlers.Occupations.ValidationRules;
using SalifyCRM.CustomerManagement.Application.RepositoryInterfaces;
using SalifyCRM.CustomerManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalifyCRM.CustomerManagement.Application.Handlers.Cities.Commands
{
    public class CreateCityCommand : IRequest<IResult>
    {
        public int CreatedUserId { get; set; }
        public int CountryId { get; set; }

        public string Name { get; set; }
        public string Status { get; set; }


        public class CreateCityCommandHandler : IRequestHandler<CreateCityCommand, IResult>
        {
            private readonly ICityRepository _cityRepository;
            private readonly ICountryRepository _countryRepository;
            private readonly IUserRepository _userRepository;
            private readonly IMediator _mediator;

            public CreateCityCommandHandler(
                ICityRepository cityRepository,
                ICountryRepository countryRepository,
                IUserRepository userRepository,
                IMediator mediator)
            {
                this._cityRepository = cityRepository;
                this._countryRepository = countryRepository;
                this._userRepository = userRepository;
                this._mediator = mediator;
            }

            public async Task<IResult> Handle(CreateCityCommand request, CancellationToken cancellationToken)
            {
                var validator = new CreateCityValidator();
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

                var isThereCountryRecord = _countryRepository.Query().Any(u => u.Id == request.CountryId);
                if (!isThereCountryRecord)
                {
                    return new ErrorResult()
                         .AddErrorDetail(new ErrorDetail
                         {
                             Code = ErrorCode.BUS4003,
                             Message = "No country was found with the provided CountryId.",
                             Title = "Country Not Found"
                         }
                        .AddMetadata("FieldName", "CountryId")
                        .AddMetadata("InputValue", request.CountryId.ToString())
                        );
                }

                City city = new City()
                {
                    Name = request.Name,
                    Status = request.Status,
                    CountryId = request.CountryId,
                    IsDeleted = false,
                    CreatedUserId = request.CreatedUserId,
                    LastUpdatedUserId = request.CreatedUserId,
                    CreatedDate = DateTime.UtcNow,
                    LastUpdatedDate = DateTime.UtcNow
                };

                _cityRepository.Add(city);
                return new SuccessResult("Request Successful.");
            }
        }
    }
}
