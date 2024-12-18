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
    public class UpdateCityCommand : IRequest<IResult>
    {
        public int Id { get; set; }
        public int LastUpdatedUserId { get; set; }
        public int CountryId { get; set; }

        public string Name { get; set; }
        public string Status { get; set; }

        public class UpdateCityCommandHandler : IRequestHandler<UpdateCityCommand, IResult>
        {
            private readonly ICityRepository _cityRepository;
            private readonly ICountryRepository _countryRepository;
            private readonly IUserRepository _userRepository;
            private readonly IMediator _mediator;

            public UpdateCityCommandHandler(
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

            public async Task<IResult> Handle(UpdateCityCommand request, CancellationToken cancellationToken)
            {
                var validator = new UpdateCityValidator();
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

                var isThereUserRecord = _userRepository.Query().Any(u => u.Id == request.LastUpdatedUserId);
                if (!isThereUserRecord)
                {
                    return new ErrorResult()
                         .AddErrorDetail(new ErrorDetail
                         {
                             Code = ErrorCode.BUS4003,
                             Message = "No user was found with the provided ID.",
                             Title = "User Not Found"
                         }
                        .AddMetadata("FieldName", "LastUpdatedUserId")
                        .AddMetadata("InputValue", request.LastUpdatedUserId.ToString())
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

                var city = await _cityRepository.GetAsync(O => O.Id == request.Id && O.IsDeleted == false);
                if (city == null)
                {
                    return new ErrorResult()
                         .AddErrorDetail(new ErrorDetail
                         {
                             Code = ErrorCode.BUS4003,
                             Message = "No city was found with the provided Id.",
                             Title = "City Not Found"
                         }
                        .AddMetadata("FieldName", "Id")
                        .AddMetadata("InputValue", request.Id.ToString())
                        );
                }

                city.CountryId = request.CountryId;
                city.Name = request.Name;
                city.Status = request.Status;

                city.LastUpdatedDate = DateTime.UtcNow;
                city.LastUpdatedUserId = request.LastUpdatedUserId;

                _cityRepository.Update(city);
                return new SuccessResult("Request Successful.");
            }
        }
    }
}
